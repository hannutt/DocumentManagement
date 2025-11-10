Project keywords: C#, WPF, .NET, File processing, SQLite

ABOUT THE APP

The idea of ​​the application is to make file processing and management easier. Currently, the app has limited features as it is in its early stages of development. Current features are listed and explained below.

AUTOMATIC SEARCH FOR FOLDERS FROM DRIVE C

When the application is launched, the listDirectories function is called. This function collects all the root directories of the C drive using the C# Directory.GetDirectories method. The found directories are stored in a list and displayed in a ComboBox element in the UI, from which the user can select a folder.

FILE SEARCH BY FILE EXTENSION

The user can search for files in a selected folder based on one or more file extensions. The user can choose whether to extend the search to subfolders or only to the parent folder using the CheckBox elements in the UI. Below is an example image where the user has retrieved only .txt and .png files from the C:\codes folder. The result is displayed in a WPF listbox element.

![Alt text](./images/file_ext_search.png)

SEARCH FILES BY PARTIAL FILENAME

The user can search for files by their partial name. For example, a file named readme.txt can be found by typing readme* in the text field of the user interface. This feature is implemented using the System.IO.DirectoryInfo and System.IO.FileInfo classes.

SEARCH FOR FILES FROM SPECIFIC FOLDERS

The user can select specific folders to search for a file. For example, the user can select folders A, B, and C and type the file name into the text box element of the user interface to search for. The selected folders are stored in a C# array and iterated over using a for loop and the Directory.GetFiles method. If a file matching the user's search is found in the folders, it is displayed in a WPF ListBox element.

USING FOUND FILES

Since the found files are displayed in a listBox element, the user can access them. Double-clicking on a file opens it using the C# Process.Start method. By right-clicking the mouse, the user can delete the selected file using the C# File.Delete method. Before deleting, the application displays a MessageBox element with yes/no buttons for the user to confirm the deletion or cancel the delete operation.

KEYBOARD SHORTCUTS FOR FILE OPERATIONS

The application has keyboard shortcuts for file operations, and all keyboard shortcut methods are in their own KeyboardCombinations class.The keyboard shortcuts and their corresponding methods are stored in a C# dictionary object. When the user presses the CTRL key and the shortcut key, the foreach loop checks which key the user pressed and calls the corresponding method using the C# Invoke method.

![Alt text](./images/combinations.png)

The keyboard shortcuts are: CTRL+K. This combination is for copying files. When the user has selected the file to be copied with a mouse click and presses ctrl+k, the application calls a method that first creates an instance of the FolderBrowserDialog class and then opens the folder structure of the C drive in graphical form.

The user can choose where to copy the selected file by selecting the folder and clicking the OK button. Finally, the copying of files is done using the Copy method of the File class.

CTRL+H = Hides the selected file.
CTRL+U = Unhides a selected file.
CTRL+I = Opens the selected image file in a separate C# WPF window.
CTRL+Z = Compress selected files into a ZIP package

ARCHIVING FILES

The application uses the Aspose.Zip library to create packages.
The user can select the desired files from the ListBox and create a ZIP package from them. The ListBox element supports multiple file selection, so the user can select the desired files at once and after selection use the CTRL+Z shortcut key combination to create a ZIP package.

After pressing the hotkey, the application uses the C# FileBrowserDialog class to open the device's folder structure in graphical mode. The user can choose where to save the completed ZIP package.
Once the selection is made, the selected files are iterated through in a foreach loop and saved to a Zip package using the Aspose.Zip Save method.

HIDING AND SHOWING FILES

The user can hide and unhide the retrieved files with a mouse click. The file path and name of each hidden file are stored in an SQLite database so that the user can track hidden files and unhide them when needed. If an originally hidden file is restored to visible, the program removes the file path from the SQLite database. Hiding and showing files are done using the SetAttribute and FileAttribute methods of the C# Files class.

DELETING FILES

The user can delete files in several ways. Once the folders and files are listed in the ListBox, the user can right-click the file they want to delete. After clicking, the application displays a MessageBox that asks the user to confirm or cancel the deletion. If the deletion is confirmed, the application deletes the selected file using the FileSystem.DeleteFile method.

Before deleting, the user can click the CheckBox to choose whether to permanently delete the file or just move it to the Recycle Bin. This feature is implemented using the Microsoft.VisualBasic.FileIO.RecycleOption method.

DELETE FILES BY FILE EXTENSION

The user can delete files by file extension. In the application user interface, the user can select the folder from which the deletion will be performed and the file extensions to be considered for deletion from a combo menu. For example, selecting the .txt and .bmp extensions will delete only the .txt and .bmp files from the selected folder. The user can use the checkbox to choose whether to search for files with the file extension to be deleted only in the main folders or also in subfolders.

