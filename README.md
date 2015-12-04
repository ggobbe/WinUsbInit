# WinUsbInit
Quick tool to copy files from a directory (no subdirectories support) to a USB drive when it is plugged and has a specified label.

## Instructions
Edit the WinUsbInit.exe.config to change the following values:

- **InitialVolumeLabel**: the label of the drive where the files will be copied when it is inserted.
- **NewVolumeLabel**: the new label the drive will receive (doesn't have to be different).
- **SourceFilesDir**: the directory containing the files that will be copied to the USB drive (no subdirectories support).

## Limitations
- The tool doesn't copy subdirectories from the **SourceFilesDir** to the USB drive.
- There is little safety code, if a folder doesn't exists or if the tool is used wrongly, it will simply crash in most cases. If it doesn't crash and doens't display any error message, then you can assume that it has done its job.

## Disclaimer
Use this tool at your own risks.
