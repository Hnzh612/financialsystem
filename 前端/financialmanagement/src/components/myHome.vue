<template>
<el-container>
  <el-header><myheader></myheader></el-header>
  <el-container>
    <el-aside width='180px'><myAside></myAside></el-aside>
    <el-main><router-view></router-view></el-main>
  </el-container>
</el-container>
</template>

<script>
import myheader from '@/components/myHeader.vue'
import myAside from '@/components/myAside.vue'

export default{
    name: 'myhome',
    components: {
      myheader,
      myAside
    }
}
</script>

<style lang="less" scoped>
  .el-aside {
    overflow: hidden!important;;
  }
  .el-row .el-container {
    height: 100%!important;
  }
</style>