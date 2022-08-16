<template>
  <div class="box">
    <!-- 左侧 logo 和 标题区域 -->
    <div class="titlebox">
      <!-- logo -->
      <!-- <img class="layout-header-left-img" src="../../assets/heart.png" alt=""> -->
      <!-- 标题 -->
      <h4 class="title">鑫恒佳耀财务管理平台</h4>
    </div>

    <!-- 右侧按钮区域 -->
    <div class="btnbox">
  <el-button type="btn" @click="logout" round>退出登录</el-button>
    </div>
  </div>
</template>

<script>
  export default {
    name:'myheader',
    data() {
      return {
      };
    },
    methods: {
          logout(){
      // 1. 清空 token
      localStorage.removeItem('token')
      // 2. 跳转到登录页面
      this.$router.push('/login')
    }
    }
  }
</script>

<style lang="less" scoped>
  .box {
    height: 60px;
    border-bottom:1px solid #e6e6e6 ;
    .titlebox {
      width: 80%;
      height: 100%;
      line-height: 60px;
      font-size: 20px;
      padding-left: 1%;
      display: inline-block;
    }
    .btnbox {
      height: 100%;
      line-height: 60px;
      display: inline-block;
      padding-left: 13%;
    }
  }
</style>