# 💬 CommunicationSystems — IPBox

A client-server communication application built with a **C++ TCP server** and a **C# desktop client**. Supports file transfer, real-time chat, and server status monitoring.

## Technologies

- C++ — TCP server, Base64 encoding
- C# / .NET — desktop client (WinForms)

## Features

- 💬 **Chat** — send messages in **unicast** (to a specific user) or **broadcast** (to all connected clients) mode
- 📤 **File upload** — send files from the client to the server
- 📥 **File download** — retrieve files stored on the server
- 📊 **Server status** — fetch real-time information about the server state (e.g. memory, CPU temperature)
- 🔐 **Base64 encoding** — used for safe data transfer over TCP

## Project Structure

```
server.cpp       → C++ TCP server
base64.cpp/.h    → Base64 encode/decode utility
ServerApp/       → C# server-side logic (optional managed layer)
ClientApp/       → C# network client logic
ChatLogic/       → Message handling, unicast/broadcast routing
GUI/             → Desktop GUI (WinForms/WPF)
```
