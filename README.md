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

USING FOUND FILES

Since the found files are displayed in a listBox element, the user can access them. Double-clicking on a file opens it using the C# Process.Start method. By right-clicking the mouse, the user can delete the selected file using the C# File.Delete method. Before deleting, the application displays a MessageBox element with yes/no buttons for the user to confirm the deletion or cancel the delete operation.

KEYBOARD SHORTCUTS FOR FILE OPERATIONS

The application has keyboard shortcuts for file operations, and all keyboard shortcut methods are in their own KeyboardCombinations class.
The keyboard shortcuts are: CTRL+K. This combination is for copying files. When the user has selected the file to be copied with a mouse click and presses ctrl+k, the application calls a method that first creates an instance of the FolderBrowserDialog class and then opens the folder structure of the C drive in graphical form.

The user can choose where to copy the selected file by selecting the folder and clicking the OK button.
Finally, the copying of files is done using the Copy method of the File class.

CTRL+H Hides the selected file. This is done using the File.SetAttribute method.