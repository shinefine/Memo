NodeJS基础资料

[七天学会NodeJS](http://www.cnblogs.com/cbscan/articles/3826461.html)




****淘宝npm镜像****

有时候npm包安装太慢，有时处于网络原因安装失败，解决方式，使用taobao源

镜像使用方法（三种办法任意一种都能解决问题，建议使用第三种，将配置写死，下次用的时候配置还在）:

1.通过config命令

npm config set registry https://registry.npm.taobao.org 
npm info underscore （如果上面配置正确这个命令会有字符串response）
2.命令行指定

npm --registry https://registry.npm.taobao.org info underscore 
3.编辑 ~/.npmrc 加入下面内容

registry = https://registry.npm.taobao.org

********全局安装模块后， require('xxx')报错 Node.js Error: Cannot find module 

解决方法 ： 设置 NODE_PATH 环境变量 值为： npm root -g 显示的路径。

如果使用webstorm 也需要在ide中配置这个环境变量