
##【安装rbenv】##
###  安装rbenv ###
可照着 [github上的文档](https://github.com/rbenv/rbenv ) 做 

安装rbenv过程中，同时需要安装 ruby-build （这样才能使用 rbenv install命令）

### 安装ruby-build 
可照着 [github上的文档](   https://github.com/rbenv/ruby-build#readme ) 做 


## 【安装 ruby 】

【 [rbenv 安装太慢的解决办法](https://ruby-china.org/topics/14564) 】

安装ruby ，由于rbenv从官方源下载ruby 太慢，所以这里才去先将ruby源码包下载到本地，将其拷贝到 ~/.rbenv/versions 目录下，然后用rbenv install 命令本地安装（需要设置 RUBY_BUILD_MIRROR_URL环境变量）  

1) wget -q http://ruby.taobao.org/mirrors/ruby/2.0/ruby-2.0.0-p247.tar.gz -O /home/shuhai/.rbenv/versions/ruby-2.0.0-p247.tar.gz

2) env RUBY_BUILD_MIRROR_URL=file:///home/shuhai/.rbenv/versions/ruby-2.0.0-p247.tar.gz# ~/.rbenv/bin/rbenv install 2.0.0-p247    (这是一条命令 不能分成两条命令执行)


##【ruby安装完成后 环境配置】

----------【 配置 rubyGems 的镜像源地址】----------------

https://gems.ruby-china.org/