# OneZip
Create zip file for modified folders with one click

## Usage

1. Create a text file named TaskList.yml in the same folder as the executable

2. Edit the file following below example

``` yaml
- Name: A Task
  SrcFolder: Folder to compress
  ZipFilePath: Output file path
  Confirmation: false
  Level: Optimal
- Name: Another Task
  SrcFolder: Folder to compress
  ZipFilePath: Output file path
  Confirmation: false
  Level: Optimal
```

3. Run the executable once you want to create lastest compression file for any of the tasks

NOTES:

Confirmation - If the output file for a task is existed in the disk, this property determines whether the program should prompt you whether to overwrite the file before the task begins

Level - possible values are NoCompression, Fastest, Optimal
