<template>
  <div v-if="this.$type == '管理员'">
    <div class="searchmean" style="margin-bottom: 10px;">
      <span class="demonstration">发货人：</span>
      <el-select v-model="querybody.sname" placeholder="请选择" style="width:100px">
        <el-option v-for="item in tableData" :key="item.id" :label="item.sname" :value="item.sname">
        </el-option>
      </el-select>
      <span class="demonstration">发货公司：</span>
      <el-select v-model="querybody.scompany" placeholder="请选择" style="width:200px">
        <el-option v-for="item in tableData" :key="item.id" :label="item.scompany" :value="item.scompany">
        </el-option>
      </el-select>
      <span class="demonstration">收货人：</span>
      <el-select v-model="querybody.rname" placeholder="请选择" style="width:100px">
        <el-option v-for="item in tableData" :key="item.id" :label="item.rname" :value="item.rname">
        </el-option>
      </el-select>
      <span class="demonstration">收货公司：</span>
      <el-select v-model="querybody.rcompany" placeholder="请选择" style="width:200px">
        <el-option v-for="item in tableData" :key="item.id" :label="item.rcompany" :value="item.rcompany">
        </el-option>
      </el-select>
      <el-button type="primary" icon="el-icon-search" circle @click="query()" style="margin-left:10px ;">查询</el-button>
      <el-button type="warning" icon="el-icon-refresh" circle @click="refresh()" style="margin-left:10px ;">刷新
      </el-button>
      <el-popover placement="bottom-left" width="980" trigger="click" style="margin:0 10px" @hide="add()">
        <div class="inline-block">
          <span class="demonstration">请选择----年份-月份：</span>
          <el-date-picker type="month" v-model="temdata.date" placeholder="选择日期" style="width:180px">
          </el-date-picker>
        </div>
        <div class="footer">
          <div class="line">
            <div class="row">
              <i>需方（签章）：</i>
              <el-input v-model="temdata.rcompany" class="input"></el-input>
            </div>
            <div class="row">
              <i>供方（签章）：</i>
              <el-input v-model="temdata.scompany" class="input"></el-input>
            </div>
          </div>
          <div class="line">
            <div class="row">
              <i>经办人：</i>
              <el-input v-model="temdata.rname" class="input"></el-input>
            </div>
            <div class="row">
              <i>经办人：</i>
              <el-input v-model="temdata.sname" class="input"></el-input>
            </div>
          </div>
          <div class="line">
            <div class="row">
              <i>电话：</i>
              <el-input v-model="temdata.rphone" class="input"></el-input>
            </div>
            <div class="row">
              <i>电话：</i>
              <el-input v-model="temdata.sphone" class="input"></el-input>
            </div>
          </div>
          <div class="line">
            <div class="row">
              <i>传真：</i>
              <el-input v-model="temdata.rfax" class="input"></el-input>
            </div>
            <div class="row">
              <i>传真：</i>
              <el-input v-model="temdata.sfax" class="input"></el-input>
            </div>
          </div>
        </div>
        <el-button type="success" icon="el-icon-plus" circle slot="reference">添加</el-button>
      </el-popover>
    </div>
    <el-table ref="singleTable" :data="tableData" highlight-current-row @current-change="handleCurrentChange"
      style="width: 100%">
      <el-table-column type="index" width="80" label="序号" align="center">
      </el-table-column>
      <el-table-column prop="date" label="日期" width="150" align="center">
      </el-table-column>
      <el-table-column prop="sname" label="发货人" width="120" align="center">
      </el-table-column>
      <el-table-column prop="sphone" label="电话" width="130" align="center">
      </el-table-column>
      <el-table-column prop="scompany" label="公司" width="250" align="center">
      </el-table-column>
      <el-table-column prop="rname" label="收货人" width="120" align="center">
      </el-table-column>
      <el-table-column prop="rphone" label="电话" width="130" align="center">
      </el-table-column>
      <el-table-column prop="rcompany" label="公司" width="250" align="center">
      </el-table-column>
      <el-table-column fixed="right" label="操作">
        <template slot-scope="scope">
          <el-button type="text" size="small" @click="edit(scope.row.rid)">编辑</el-button>
          <el-button @click="deleteSalesConnect(scope.row)" type="text" size="small">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <pages :total="total" :currentpage="querybody.page" @handleSizeChangeSub="handleSizeChangeFun"
      @handleCurrentChangeSub="handleCurrentChangeFun">
    </pages>
  </div>
  <notype v-else></notype>
</template>

<script>
import salesApi from '@/api/salesApi';
import pages from '../utlis/pages.vue';
import notype from '@/components/utlis/notype.vue'

export default {
  components: { 
    pages,
    notype
  },
    data() {
    return {
      total:11, // 总数
      // 查询体
      querybody: {
        page:1,
        sname: "",
        scompany: "",
        rname: "",
        rcompany: ""
      },
      tableData: [],
      // 缓存数据
      temdata: {
        id: 0,
        date: new Date().getTime(),
        sid: 0,
        sname: "韩侃",
        sphone: "18685231125",
        sfax: "",
        scompany: "遵义市鑫恒佳耀贸易有限公司",
        rid: 0,
        rname: "",
        rphone: "",
        rfax: "",
        rcompany: ""
      }
    }
  },
  methods: {
    // 页码点击
    handleSizeChangeFun(v) {
      console.log(v)
    },

    handleCurrentChangeFun(v) { //页面点击
      console.log(v)
      this.querybody.page = v; //当前页
      this.getAllSalesConnect(this.querybody)
    },
    // 删除订单
    async deleteSalesConnect(row) {
      this.$confirm('此操作将永久删除该订单，是否继续？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        const { data: res } = await salesApi.deleteSalesConnectById(row.id)
        if (res == 1) {
          this.$message({
            message: '删除成功',
            type: 'success'
          })
          if(this.querybody.page != 1 &&this.total %10 == 1) {
            this.querybody.page = this.querybody.page - 1
          }
          this.getAllSalesConnect(this.querybody)
        } else {
          this.$message({
            message: '删除失败',
            type: 'success'
          })
        }
      }).catch(() => {
        this.$message({
          message: '已取消删除',
          type: 'info'
        })
      })
    },
    // 编辑
    edit(cid) {
      this.$router.push('/home/salesdetail/' + cid)
    },
    // 添加
    add() {
      this.temdata.date = this.dayjs(this.temdata.date).$d.getTime()
      if (this.temdata.rcompany != '') {
        this.addSalesConnect(this.temdata)
        this.temdata = {
          id: 0,
          date: new Date().getTime(),
          sid: 0,
          sname: "韩侃",
          sphone: "18685231125",
          sfax: "",
          scompany: "遵义市鑫恒佳耀贸易有限公司",
          rid: 0,
          rname: "",
          rphone: "",
          rfax: "",
          rcompany: ""
        }
      } else {
        this.$message({
          message: "请将信息完善！",
          type: "error",
          customClass: 'message',
          center: true,
          duration: 1500
        })
      }

    },
    // 查询
    query() {
      this.getAllSalesConnect(this.querybody)
    },
    // 刷新
    refresh() {
      this.querybody = {
        page:1,
        sname: "",
        scompany: "",
        rname: "",
        rcompany: ""
      }
      this.getAllSalesConnect(this.querybody)
    },
    setCurrent(row) {
      this.$refs.singleTable.setCurrentRow(row);
    },
    handleCurrentChange(val) {
      this.currentRow = val;
    },
    // 获取双方信息
    async getAllSalesConnect(querybody) {
      const { data: res } = await salesApi.getAllSalesConnect(querybody)
      for (let i = 0; i < res.data.length; i++) {
        this.$set(
          res.data[i], 'date', this.dayjs(res.data[i].date).format('YYYY-MM')
        )
      }
      this.tableData = res.data
      this.total = res.message*1
    },
    // 添加订单
    async addSalesConnect(temdata) {
      const { data:res} = await salesApi.addSalesConnect(temdata)
      if(res==1) {
        this.getAllSalesConnect(this.querybody)
      }
    },
  },
  created() {
    this.getAllSalesConnect(this.querybody)
  }
}
</script>

<style lang="less" scoped>
.inline-block {
    display: inline-block;
    margin: 0 5px 5px 0;
    height: 40px;
}
.footer {
  width: 955px;
  background-color: #F5F7FA;

  .row {
    display: inline-block;
    width: 50%;
    border: 1px solid #ebeef5;
  }

  i {
    margin-left: 5%;
    width: 30%;
  }

  .input {
    width: 50%;
  }

  .input/deep/.el-input__inner {
    background-color: #f5f7fa;
    border: 0;
  }
}

.demonstration {
  margin-left: 10px;
  display: inline-block;
}
</style>