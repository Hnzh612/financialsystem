<template>
<div>
    <img src="@/assets/notype.png" class="notype">
    <p class="words">抱歉！您无权访问该页面，请联系管理员</p>
</div>
</template>


<script>
export default{
    name:"notype"
}
</script>


<style lang="less" scoped>
    .notype {
        width: 300px;
        margin:4% calc(50% - 150px)
    }
    .words {
        text-align: center; 
        margin-top: 50px; 
        font-size: 18px;
    }
</style>