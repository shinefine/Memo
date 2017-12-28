[将js代码翻译成颜文字代码 （可用于混淆加密代码）](
http://utf-8.jp/public/aaencode.html)



【在主机环境（非浏览器）下执行js代码】

一般js代码实在浏览器环境下解释执行的，若要在主机环境下（cmd 窗口）执行，可以安装 node.js ,然后创建一个js文件(test.js)，里面写好你想要执行的js代码，在cmd窗口中使用 node test.js 即可执行js代码，看到输出结果。    这个特性用于学习es6 有帮助，因为可以直接在cmd环境下而非不用总是在浏览器里去运行js代码。

【node.js介绍】
	[Node.js是用来做什么的?--知乎](https://www.zhihu.com/question/33578075)



Js 语言特性 promise

[Promise特性介绍](http://www.cnblogs.com/rubylouvre/p/3495286.html#toc-async)
补充：

 问：为什么 new promise () 的参数是个函数 somefunc（该函数带两个参数），而非直接定义两个参数(一个参数对应成功回调函数succ_callback，另一个对应失败回调函数 err_callback) 

答： 因为 promise 承若的代码需要 延迟执行， 所以讲执行代码包装进函数中，在promise 对象里保存这个函数，才能达到延时执行的效果（具体是在调用 promise对象的then（）方法时执行定义时传入的函数）[这里描述不正确，实际上，promise对象new出来时就已经执行了内部的异步代码，调用then（）方法只是取得异步代码的执行结果而已。]

另： 调用then（）方法时传入的两个函数，分别处理成功和失败两种逻辑，在then（）内部实现中，相当于调用 promise.new() 时传入的的那个函数，并将这两个函数作为目标函数(somefunc)的参数传入  

通俗来说就是 ，promise对象 的new（） 和 then() 这两个方法联合起来搭建了一种机制，使用这种机制你需要传给它三个函数，一个是延时执行的函数 （当做 new()的参数传入），另外两个是 成功回调函数，失败回调函数（作为then()的参数传入） ，这种机制保证了 延时执行函数会被执行，且执行完后根据结果执行 成功回调函数或失败回调函数。注意，这个机制不会去判断 延时函数的执行结果，判断结果的成功或失败是在延时函数自身内部做的。

  promise机制起到的作用很简单： 就是 延时执行一个函数；

并

将延时函数和 成功/失败 回调函数 联系起来，使得延时函数执行完成后能够回调一个成功/失败 函数。



----------


[【谈谈使用promise时候的一些反模式】](http://www.tuicool.com/articles/FvyQ3a)Promise  几种不同的写法的细微区别，以及如何在一个方法中处理两个promise的返回结果
[(link2)](http://mp.weixin.qq.com/s?__biz=MzIyMzAwNzExNg==&mid=209354478&idx=1&sn=edd70e826b6f9e8a570024f431c5f7fe&scene=1&key=c76941211a49ab58efed75a0405e3ca61338952103fe9eabf8528d801906e4522737274eecca5489d635a5c1aa5d8b12&ascene=0&uin=MTYxMDY3MjU1&devicetype=iMac+MacBookPro11%2C3+OSX+OSX+10.10.4+build(14E46)&version=11020113&pass_ticket=ws1Ar8vSXgH8%2FuRvUaFkiKCA57pR8100%2BhwA5Ifuc00%3D)


[【Javascript 中的神器——Promise】](http://www.jianshu.com/p/063f7e490e9a)介绍了Promise 的几个api函数 join，map，race，all，any

[promise详细解释](http://liubin.org/promises-book/#introduction)

------------------

懂了promise后，进阶学习await async 语法

[ 体验异步的终极解决方案-ES7的Async/Await](http://cnodejs.org/topic/5640b80d3a6aa72c5e0030b6)
看完后再结合看      
[ 关于JavaScript 的 async/await](http://blog.csdn.net/hj7jay/article/details/61191416)
基本就弄清楚 async await 的用法了