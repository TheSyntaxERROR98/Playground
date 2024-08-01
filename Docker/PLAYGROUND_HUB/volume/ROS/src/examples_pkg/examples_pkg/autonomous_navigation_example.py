from typing import Tuple, Type
import time

import gymnasium as gym
import numpy as np
import rclpy

from playground_pkg.single_agent_environment_node import SingleAgentEnvironmentNode
from playground_pkg.utils.pose_converter import PoseConverter
from interfaces_pkg.srv import AutonomousNavigationExampleEnvironmentAction, AutonomousNavigationExampleEnvironmentState, AutonomousNavigationExampleEnvironmentReset
from playground_pkg.utils.communication_monitor import CommunicationMonitor

import numpy as np
import matplotlib.pyplot as plt
import stable_baselines3 as sb3
from scipy.spatial.transform import Rotation as R

from stable_baselines3.common.env_checker import check_env
from stable_baselines3 import PPO


class AutonomousNavigationExampleEnvironment(SingleAgentEnvironmentNode):

    def __init__(self, sample_time: float):

        # ROS initialization
        SingleAgentEnvironmentNode.__init__(
            self,
            environment_name='autonomous_navigation_example_environment',
            action_service_msg_type=AutonomousNavigationExampleEnvironmentAction,
            state_service_msg_type=AutonomousNavigationExampleEnvironmentState,
            reset_service_msg_type=AutonomousNavigationExampleEnvironmentReset,
            sample_time=sample_time
        )

        # Environment parameters
        self.max_relative_target_distance = 5.0
        self._previous_target_relative_distance = None
        self._current_target_relative_distance = None
        self._min_linear_velocity = -1.0
        self._max_linear_velocity = 3.0
        self._max_yaw_rate = 3.0


    def convert_action_to_request(self, action: np.ndarray = None) -> AutonomousNavigationExampleEnvironmentAction.Request:
        
        # action = np.array([linear_velocity, yaw_rate])
        # AutonomousNavigationExampleEnvironmentAction.Request:
        # geometry_msgs/Twist twist

        # Scale the action to the range [self._min_linear_velocity, self._max_linear_velocity] when action[0] is in the range [-1.0, 1.0]
        linear_velocity = (action[0] + 1.0) * (self._max_linear_velocity - self._min_linear_velocity) / 2.0 + self._min_linear_velocity
        yaw_rate = action[1] * self._max_yaw_rate

        # Set the agent linear velocity and yaw rate
        self.action_request.action.twist.linear.x = linear_velocity
        self.action_request.action.twist.angular.y = yaw_rate

        return self.action_request


    def convert_response_to_state(self, response: AutonomousNavigationExampleEnvironmentState.Response) -> dict:

        # AutonomousNavigationExampleEnvironmentStep.Response:
        # geometry_msgs/Pose pose
        # geometry_msgs/Pose target_pose
        # sensor_msgs/LaserScan laser_scan
        # TriggerSensor collision_trigger_sensor
        # TriggerSensor target_trigger_sensor

        # Convert the response to dict
        state = {

            'pose': {
                'position': np.array([
                    response.state.pose.position.x,
                    response.state.pose.position.y,
                    response.state.pose.position.z,
                ]),
                'orientation': np.array([
                    response.state.pose.orientation.x,
                    response.state.pose.orientation.y,
                    response.state.pose.orientation.z,
                    response.state.pose.orientation.w,
                ])
            },

            'target_pose': {
                'position': np.array([
                    response.state.target_pose.position.x,
                    response.state.target_pose.position.y,
                    response.state.target_pose.position.z,
                ]),
                'orientation': np.array([
                    response.state.target_pose.orientation.x,
                    response.state.target_pose.orientation.y,
                    response.state.target_pose.orientation.z,
                    response.state.target_pose.orientation.w,
                ])
            },

            'laser_scan': {
                'ranges': np.array(response.state.laser_scan.ranges),
                'angle_min': response.state.laser_scan.angle_min,
                'angle_max': response.state.laser_scan.angle_max,
                'angle_increment': response.state.laser_scan.angle_increment,
                'range_min': response.state.laser_scan.range_min,
                'range_max': response.state.laser_scan.range_max
            },

            'collision_trigger_sensor': {
                'has_triggered': response.state.collision_trigger_sensor.has_triggered,
                'has_timer_finished': response.state.collision_trigger_sensor.has_timer_finished,
                'timer_count': response.state.collision_trigger_sensor.timer_count,
                'max_timer_count': response.state.collision_trigger_sensor.max_timer_count
            },

            'target_trigger_sensor': {
                'has_triggered': response.state.target_trigger_sensor.has_triggered,
                'has_timer_finished': response.state.target_trigger_sensor.has_timer_finished,
                'timer_count': response.state.target_trigger_sensor.timer_count,
                'max_timer_count': response.state.target_trigger_sensor.max_timer_count
            }

        }

        return state
    

    def convert_reset_to_request(self) -> AutonomousNavigationExampleEnvironmentReset.Request:

        # AutonomousNavigationExampleEnvironmentReset.Request:
        # geometry_msgs/Pose agent_target_pose
        # geometry_msgs/Pose target_target_pose

        # Reset the agent pose
        self.reset_request.reset_action.agent_target_pose.position.x = np.random.choice([-12.5, -7.5, 7.5, 12.5])
        self.reset_request.reset_action.agent_target_pose.position.y = 1.0
        self.reset_request.reset_action.agent_target_pose.position.z = np.random.choice([-12.5, -7.5, 7.5, 12.5])

        # Reset the target pose
        self.reset_request.reset_action.target_target_pose.position.x = np.random.choice([-10.0, -5.0, 5.0, 10.0])
        self.reset_request.reset_action.target_target_pose.position.y = 1.0
        self.reset_request.reset_action.target_target_pose.position.z = np.random.choice([-10.0, -5.0, 5.0, 10.0])

        return self.reset_request  
    

    def observation(self, state: np.ndarray) -> np.ndarray:

        # Get the target relative position in the global coordinate system
        target_relative_position = PoseConverter.ros_to_unity_vector(
            state['target_pose']['position'] - state['pose']['position']
        )

        # Get the euler angles
        euler_angles, rotation = PoseConverter.unity_quaternion_to_ros_euler(state['pose']['orientation'])
        yaw = euler_angles[2]

        # Rotate the target relative position
        target_relative_position = rotation.apply(target_relative_position)

        # Remove the z component
        target_relative_position = np.array([
            target_relative_position[0],
            target_relative_position[1],
        ])

        # Normalize the target relative position
        target_relative_position = target_relative_position / self.max_relative_target_distance
        self._current_target_relative_distance = np.linalg.norm(target_relative_position)

        # Limit the target relative distance to 1.0
        if self._current_target_relative_distance > 1.0:
            target_relative_position = target_relative_position / self._current_target_relative_distance

        # Normalize the yaw to [-1.0, 1.0]
        yaw = yaw / np.pi

        # Get and min-max normalize the lidar data
        # lidar_ranges = (state['laser_scan']['ranges'] - state['laser_scan']['range_min']) / (state['laser_scan']['range_max'] - state['laser_scan']['range_min'])

        # Get the combined observation
        observation = np.concatenate([
            target_relative_position,
            [yaw],
            # lidar_ranges
        ])

        return observation
    

    def reward(self, state: np.ndarray, action: np.ndarray = None) -> float:

        # Check if the agent has moved closer to the target
        if self._previous_target_relative_distance is not None:
            reward = 0.1 * np.sign(self._previous_target_relative_distance - self._current_target_relative_distance)
        else:
            reward = 0.0

        if state['target_trigger_sensor']['has_triggered']:
            reward = 10.0

        # Update the previous target relative distance
        self._previous_target_relative_distance = self._current_target_relative_distance

        return reward
    

    def terminated(self, state: np.ndarray) -> bool:
        
        has_collided = state['collision_trigger_sensor']['has_triggered']
        has_reached_target = state['target_trigger_sensor']['has_triggered']

        terminated = has_collided or has_reached_target

        return terminated
    

    def truncated(self, state: np.ndarray) -> bool:
        return False


    def info(self, state: np.ndarray) -> dict:
        return {}
    

    def render(self):
        pass


class GymEnvWrapper(gym.Env):

    def __init__(self, env: Type[AutonomousNavigationExampleEnvironment]):

        # Environment initialization
        self.env = env

        # Environment parameters
        self.observation_space = gym.spaces.Box(
            low=-np.inf,
            high=np.inf,
            shape=(3,),
            dtype=np.float32
        )

        self.action_space = gym.spaces.Box(
            low=-1.0,
            high=1.0,
            shape=(2,),
            dtype=np.float32
        )

        self.reward_range = (-np.inf, np.inf)
    

    def step(self, action: np.ndarray) -> Tuple[np.ndarray, float, bool, dict]:
        observation, reward, terminated, truncated, info = self.env.step(action)

        # Cast the observation from float64 to float32
        observation = observation.astype(np.float32)

        return observation, reward, terminated, truncated, info
    

    def reset(self, **kwargs) -> Tuple[np.ndarray, dict]:

        observation, info = self.env.reset()

        # Cast the observation from float64 to float32
        observation = observation.astype(np.float32)

        return observation, info
    

    def render(self):
        return self.env.render()
    

    def close(self):
        return self.env.close()




def main():

    rclpy.init()

    sample_time = 2.0

    base_env = AutonomousNavigationExampleEnvironment(sample_time=sample_time)
    env = GymEnvWrapper(base_env)
    communication_monitor = CommunicationMonitor(base_env)

    # # Check the environment
    # check_env(env)

    normalize = True
    n_timesteps = 5e6
    policy = 'MlpPolicy'
    n_steps = 2048
    batch_size = 64
    gae_lambda = 0.95
    gamma = 0.999
    n_epochs = 10
    ent_coef = 0.0
    learning_rate = 3e-4
    clip_range = 0.18

    # Create the agent
    model = PPO(
        policy,
        env,
        verbose=1,
        learning_rate=learning_rate,
        n_steps=n_steps,
        batch_size=batch_size,
        n_epochs=n_epochs,
        gamma=gamma,
        gae_lambda=gae_lambda,
        clip_range=clip_range,
        ent_coef=ent_coef,
        normalize_advantage=normalize,
    )

    # Train the agent
    # model.learn(total_timesteps=int(n_timesteps))


    env.reset()
    action = np.array([0.0, 0.0])

    

    while True:
        
        # init_time = time.time()
        state, reward, terminated, truncated, info = env.step(action)
        # print('Step time:', time.time() - init_time)

        communication_monitor.update()
        communication_monitor.visualize()

        # print(f'State: {state}')
        action = np.random.uniform(-1.0, 1.0, 2)

        if terminated or truncated:
            env.reset()

        time.sleep(1.0)




    env.close()


if __name__ == '__main__':
    main()
