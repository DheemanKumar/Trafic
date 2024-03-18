# sum_numbers_server.py
import socket
import Bandet

def sum_two_numbers(num):
    return sum(num)

# Define host and port
HOST = '127.0.0.1'
PORT = 12345

# Create a socket object
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the host and port
server_socket.bind((HOST, PORT))

# Listen for incoming connections
server_socket.listen()

print(f"Server is listening on {HOST}:{PORT}")

# Accept incoming connection
client_socket, addr = server_socket.accept()
print(f"Connection from {addr} has been established.")

while True:
    # Receive data from client
    data = client_socket.recv(1024)
    if not data:
        break
    # Decode the received data
    Bandet.convertor(data)
    
    result = 1
    # Send the result back to the client
    client_socket.send(str(result).encode())

# Close the connection
client_socket.close()
server_socket.close()
