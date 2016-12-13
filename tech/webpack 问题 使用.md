webpack 安装使用命令 

npm install webpack -g

安装后使用webpack会报错
Cannot find module 'webpack/lib/node/NodeTemplatePlugin'
这是由于环境变量没有指定 NODE_PATH 导致，

解决方法:

第一步：npm config get prefix ，获取输出path“C:\Users\jaxGu\AppData\Roaming\npm”加上"\node_modules"用于第二步值

第二步：添加系统环境变量：NODE_PATH:C:\Users\jaxGu\AppData\Roaming\npm\node_modules

第三步：关掉命令行，重新打开。