function  *simpleCombination(arr){
    yield [arr[0],arr[1]];
    return [arr[1],arr[0]];

}


let getSomeCombinationValue  =   simpleCombination([1,2]);

while(1)
{
    let v = getSomeCombinationValue.next();
    let [PHasFeature,PCategoryBelong] =v.value;
    console.log([PHasFeature,PCategoryBelong]);
    if(v.done){
        break;
    }

}
console.log("ok")