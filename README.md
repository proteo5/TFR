# Temporary File Remover (TFR)

Temporary File Remover (TFR) is a console application designed to help you regularly delete temporary files and folders on your system. It reads a list of file paths from a specified text-based paths file and deletes those files and the folders inside of them, optionally skipping confirmation prompts.

## Features
- Deletes files and folders based on a list of paths provided in a text file.
- Option to skip delete confirmation prompts.
- Displays help and version information.

## Usage

```sh
TFR [--skip-confirmation] [--help] [--version] path-file
```

## Arguments
- path-file: Specify a paths file containing the list of files to delete. If no path is provided then it will look for the file paths.txt.

## Options
- -s, --skip-confirmation: Skip the delete confirmation prompt.
- -h, --help: Show help message.
- --version: Show version information.

## Example

```
TFR /path/to/your/paths-file.txt
```

## Paths File Format
The paths file should contain one file path per line.  
Also they can contain comments by starting the line with the "#" caracter.  
Empty lines will be ignored.  
For example:

```
# Group 1
C:\Temp\file1.tmp
C:\Temp\file2.tmp
C:\Temp\file3.tmp

# Group 2
C:\Temp2\file1.tmp
```
## Acknowledgments

* Thanks to [Mayuki](https://github.com/mayuki) for your project Cocona [https://github.com/mayuki/Cocona](https://github.com/mayuki/Cocona), doing the console app with your framework was a breeze.
* Thanks for the icon to Flaticon [Delete folder icon created by juicy_fish - Flaticon](https://www.flaticon.com/free-icons/delete-folder)

## Authors

- Alfredo Pinto Molina (aka proteo5) [https://github.com/proteo5](https://github.com/proteo5)

## License
This project is licensed under the MIT License.

Copyright 2024 Alfredo Pinto Molina

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.