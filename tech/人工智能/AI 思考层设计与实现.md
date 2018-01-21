！[1](#jump)



基础概念表/常识表 （存储抽象常识范式 记录）
【baseConcept】

<table>
	<tr>
		<td>id
		</td>
		<td>main_name
		</td>
		<td>main_composite_id
		</td>
		<td>client_name
		</td>
		<td>client_composite_id
		</td>
		<td>link_name
		</td>
	</tr>
	<tr>
				<td>id
		</td>
		<td>主体名
		</td>
		<td>复合主体id
		</td>
		<td>客体名
		</td>
		<td>复合客体id
		</td>
		<td>关系/链接名 ，用于描述主体和客体之间的关系。
		</td>
	</tr>	
	<tr><td>举例</td></tr>

	<tr>
				<td>0001
		</td>
		<td>人
		</td>
		<td>
		</td>
		<td>食物
		</td>
		<td>
		</td>
		<td>吃
		</td>
	</tr>
	<tr>
				<td>0002
		</td>
		<td>人
		</td>
		<td>
		</td>
		<td>人
		</td>
		<td>
		</td>
		<td>打
		</td>
	</tr>		

</table>


	



在每一条记录中 link_name 一定有值， main_name 和  main_composite_id这两个字段一定有其中一个字段有值，但不会都无值或都有值 ，client字段同理。


人--吃-- 食物
人---打---人 

示例中的 **人**，**食物** 都是泛华集合概念

在此表中，
一般我们不存储 “小明吃饭”“门卫打流氓”这种详细的概念，而是存储 人--吃-- 食物， 人---打---人 这种抽象常识范式。
**小明**， **米饭** 分别是 类属概念 “人”“食物”的特化

------------------------------
严格来说 ，
**小明**   是名字集合的一个元素   （根据）
**饭**     是 米饭的简称， 同时可代表 饭菜 的意义  (一个字 可以是多个词的简化称谓)








----------------------------------------------------
【归类表】








<span id="jump">这里</span>
范德萨发生