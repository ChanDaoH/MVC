# MVC
这是根据网上的教程进行修改过后，基于MVC框架的，管理信息系统模板。










Tips:
----
今天调试的时候发现，Chrome+Visual Studio调试Asp.net程序特别慢，特地查了下，找到以下解决方案

>在C:/Windows/System32/drivers/etc里边有一个hosts文件，用记事本打开，里边最后几行原来是：

>     # localhost name resolution is handled within DNS itself.
>     #	127.0.0.1       localhost
>     #	::1             localhost

>  删掉127前面的*，改成：

>     # localhost name resolution is handled within DNS itself.
>     127.0.0.1       localhost
>     #	::1             localhost

**效果就会立竿见影了: )**

