GUID Generator Console App
==========================

Overview
--------
This project is a C# console application designed to generate unique GUIDs 
(Globally Unique Identifiers) with added functionality for persistence, 
blacklist management, and clipboard integration. It provides a visually 
distinct console output with color-coded segments for readability.

The application was written with the assistance of Microsoft Copilot.

Features
--------
- Generate Unique GUIDs
  Press Enter to generate a new GUID that is guaranteed not to be duplicated 
  or blacklisted.

- Copy to Clipboard
  Press Spacebar to copy the most recently generated GUID directly to your 
  clipboard.

- Blacklist Management
  * GUIDs are automatically added to a blacklist file (blacklisted_guids.txt) 
    to prevent reuse.
  * If a blacklist file is missing, the app warns the user but continues 
    execution.

- Persistent Storage
  * All generated GUIDs are stored in generated_guids.txt along with a 
    timestamp.
  * This ensures reproducibility and traceability of GUIDs across sessions.

- Color-Coded Display
  GUID segments are displayed in different colors for better visual distinction:
    Cyan, Green, Yellow, Magenta, White

- Menu System
  A clear, text-based menu shows available commands and statistics about 
  generated and blacklisted GUIDs.

Usage
-----
1. Run the console application.
2. Use the following keys:
   - Enter   : Generate a new GUID
   - Space   : Copy the current GUID to clipboard
   - Escape  : Exit the application
3. GUIDs are automatically saved and blacklisted to prevent duplicates.

File Outputs
------------
- generated_guids.txt   : Stores all generated GUIDs with timestamps.
- blacklisted_guids.txt : Stores GUIDs that should not be reused.

Notes
-----
- GUID collisions are extremely rare, but the blacklist and persistence 
  mechanisms ensure uniqueness across sessions.
- The application uses the TextCopy library for clipboard integration.
- Written with Copilot assistance for maintainability and clarity.
