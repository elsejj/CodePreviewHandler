# Description

`CodePreviewHandler` is a source code highlighter previewer plugin for windows file explorer, it's base on  [Power Toys Preview Pane](https://github.com/microsoft/PowerToys/tree/master/src/modules/previewpane), and use [Highlight ](http://www.andre-simon.de/doku/highlight/en/highlight.php) as source code highlight. so before use this tool, your must install

- [Power Toys](https://github.com/microsoft/PowerToys)
- [Highlight ](http://www.andre-simon.de/doku/highlight/en/highlight.php)

and [enable preview pane.](https://www.dummies.com/computers/operating-systems/windows-10/how-to-enable-and-use-panes-in-windows-10/)

![SceenShot](meta/screen.png)



# How to install

1. Copy `CodePreviewHandler.dll` to `Power Toys` install folder,  it's default is `C:\Program Files\PowerToys\modules`
2. import `meta\install_code_preview.reg`  (double click)to Registry, it will let windows file explorer known there is a code previewer. if your changed Power Toys install path, you should also edit the reg file point where your install folder.
3. import `meta\associate_file_to_preview.reg` (double click) to Registry, it will associate some source code file to this previewer.  you may edit this reg file,  remove some source file your don't want associate.

# How to develop

1. Checkout [Power Toys source ](https://github.com/microsoft/PowerToys)
2. `cd src\modules\previewpane`
3. Checkout this source
4. Open PowerTosy.sln, add this project to  `previewpane` project.

