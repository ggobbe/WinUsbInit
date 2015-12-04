# WinUsbInit
Quick tool to copy files (no subdirectories support) to a pugged USB drive.

## Instructions
Edit the WinUsbInit.exe.config to change the following values:

- **InitialVolumeLabel**: the volume label of the drive that will be inserted.
- **NewVolumeLabel**: the new volume label that the drive will receive.
- **SourceFilesDir**: the directory containing the files that will be copied to the USB drive.

## Limitations
- The tool doesn't copy subdirectories of the **SourceFilesDir**.
- There is little safety code, if a folder doesn't exists or if the tool is used wrongly, it will simply crash in most cases.
