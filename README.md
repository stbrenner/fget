# FGet
Command line file downloader for Windows

It can resme aborted downloads and thus makes it possible to download even big files via bad internet connections. Internally the [.NET File Downloader](https://github.com/Avira/.NetFileDownloader) library is used.

## Download
[fget_1.1.zip](../../releases/download/v1.1/fget_1.1.zip)

## Usage
```
fget [url] [option]

Options:
  -h        Print this help
  -o path   Write file to the given path
```

## Examples
Download file into the current directory with retaining the file name:
```
> fget https://raw.githubusercontent.com/stbrenner/fget/master/README.md
```

Save downloaded file under a specific path:
```
> fget https://raw.githubusercontent.com/stbrenner/fget/master/README.md -o c:\windows\temp\MyFile.md
```
