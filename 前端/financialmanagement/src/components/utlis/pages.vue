<template>
  <el-pagination 
    @size-change="handleSizeChange" 
    @current-change="handleCurrentChange" 
    layout="prev, pager, next" 
    :total="total"
    :current-page="currentpage" 
    style="float:left;"
    background
    >
  </el-pagination>
</template>

<script>
export default {
  name: 'pages',
  props: {
    pageSize: {
      type: Number,
      default: 10
    },
    total: {
      type: Number,
      default: 1
    },
    currentpage: {
      type: Number,
      default:1
    }
  },
  data() {
    return {

    }
  },
  methods: {
    //每页展示条数
    handleSizeChange(val) {
      //事件传递
      this.$emit('handleSizeChangeSub', val);
    },
    //当前页
    handleCurrentChange(val) {
      //事件传递
      this.$emit('handleCurrentChangeSub', val);
    }
  }
}
</script>