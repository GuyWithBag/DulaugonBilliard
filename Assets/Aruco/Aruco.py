import cv2
import cv2.aruco as aruco
import numpy as np
import socket
import json
import time

# UDP setup
UDP_IP = "127.0.0.1"
UDP_PORT = 5065
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

def send_to_unity(marker_id, position, rotation):
    data = {
        "marker_id": int(marker_id),
        "position": position,
        "rotation": rotation  # Now sending rotation as Euler angles
    }
    try:
        sock.sendto(json.dumps(data).encode(), (UDP_IP, UDP_PORT))
    except Exception as e:
        print(f"Network error: {e}")

def main():
    cap = cv2.VideoCapture(0)
    if not cap.isOpened():
        print("Error: Could not open video device.")
        return

    # Camera parameters - adjust these to your calibration
    camera_matrix = np.array([[1000, 0, 640], [0, 1000, 360], [0, 0, 1]])
    dist_coeffs = np.zeros((4, 1))
    marker_length = 0.05  # 5cm in meters

    last_send_time = time.time()
    send_interval = 0.016  # ~60 FPS
    position_scale = 15.0
    rotation_scale = 1.0  # Adjust if rotations are too large/small

    print("Press 'q' to quit...")
    
    while True:
        ret, frame = cap.read()
        if not ret: break

        frame = cv2.resize(frame, (640, 480))
        aruco_dict = aruco.getPredefinedDictionary(aruco.DICT_5X5_250)
        parameters = aruco.DetectorParameters()
        detector = aruco.ArucoDetector(aruco_dict, parameters)
        corners, ids, _ = detector.detectMarkers(frame)

        if ids is not None and (time.time() - last_send_time) > send_interval:
            for i in range(len(ids)):
                obj_points = np.array([
                    [-marker_length/2, marker_length/2, 0],
                    [marker_length/2, marker_length/2, 0],
                    [marker_length/2, -marker_length/2, 0],
                    [-marker_length/2, -marker_length/2, 0]
                ], dtype=np.float32)

                success, rvec, tvec = cv2.solvePnP(
                    obj_points,
                    corners[i].reshape(-1, 2),
                    camera_matrix,
                    dist_coeffs,
                    flags=cv2.SOLVEPNP_IPPE_SQUARE
                )

                if success:
                    # Convert rotation vector to Euler angles
                    rot_mat, _ = cv2.Rodrigues(rvec)
                    euler_angles = cv2.RQDecomp3x3(rot_mat)[0]
                    
                    # Scale and convert to Unity coordinates
                    unity_pos = [
                        tvec[0][0] * position_scale,
                        -tvec[1][0] * position_scale,  # Y-up conversion
                        tvec[2][0] * position_scale
                    ]
                    
                    unity_rot = [
                        euler_angles[0] * rotation_scale,  # Pitch (X)
                        -euler_angles[1] * rotation_scale, # Yaw (Y) (inverted)
                        euler_angles[2] * rotation_scale   # Roll (Z)
                    ]

                    send_to_unity(ids[i][0], unity_pos, unity_rot)
                    print(f"Sent: Pos {unity_pos}, Rot {unity_rot}")

            last_send_time = time.time()

        cv2.imshow('Markers', aruco.drawDetectedMarkers(frame.copy(), corners, ids))
        if cv2.waitKey(1) == ord('q'): break

    cap.release()
    cv2.destroyAllWindows()
    sock.close()

if __name__ == "__main__":
    main()
