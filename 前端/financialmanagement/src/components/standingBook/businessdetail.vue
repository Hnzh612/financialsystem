<template>
  <div v-if="this.$type == '管理员'">
    <div class="searchmean" style="margin-bottom: 10px;">
      <el-button type="warning" icon="el-icon-check" circle @click="save()">保存</el-button>
      <el-button type="danger" icon="el-icon-delete" circle @click="dedeleteSalesOrder(curid)">删除</el-button>
      <el-button type="primary" icon="el-icon-upload el-icon--right" circle @click="exportExcel">导出</el-button>
      <el-popover placement="right" width="350" trigger="click" style="margin:0 10px" @hide="add()" v-model="show">
        <div class="inline-block">
          <span class="demonstration">日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;期：</span>
          <el-date-picker type="date" v-model="temdata.date" placeholder="选择日期" style="width:180px">
          </el-date-picker>
        </div>
        <div class="inline-block">
          <span class="demonstration">买&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;方：</span>
          <el-input v-model="temdata.buyer" style="width:180px"></el-input>
        </div>
        <div class="inline-block">
          <span class="demonstration">进&nbsp;(出)账：</span>
          <el-input v-model="temdata.incoin" style="width:180px"></el-input>
        </div>
        <div class="inline-block">
          <span class="demonstration">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</span>
          <el-input v-model="temdata.remark" style="width:180px"></el-input>
        </div>
        <div style="text-align:center;">
          <el-button type="success" circle @click="showoff()">确定</el-button>
        </div>

        <el-button type="success" icon="el-icon-plus" circle slot="reference">添加</el-button>
      </el-popover>
      <el-button type="info" icon="el-icon-arrow-left" circle @click="back()">返回</el-button>
    </div>
    <el-table :data="tableData" style="width: 100%;" :summary-method="getSummaries" show-summary tooltip-effect="dark"
      @selection-change="handleSelectionChange" id="business">
      <el-table-column type="selection" width="55">
      </el-table-column>
      <el-table-column label="遵义市鑫恒佳耀贸易有限公司业务发生台账" align="center">
        <el-table-column prop="date" label="日期" width="150" align="center">
        </el-table-column>
        <el-table-column prop="buyer" label="购买方" width="100" align="center">
        </el-table-column>
        <el-table-column prop="incoin" label="进账" width="100" align="center">
        </el-table-column>
        <el-table-column prop="outcoin" label="出账" width="100" align="center">
        </el-table-column>
        <el-table-column prop="surplus" label="余额" width="120" align="center">
        </el-table-column>
        <el-table-column prop="remark" label="备注" width="90" align="center">
        </el-table-column>
      </el-table-column>
    </el-table>
  </div>
  <notype v-else></notype>
</template>


<script>
import pages from '@/components/utlis/pages.vue'
import businessledgerApi from '@/api/businessledgerApi'
import FileSaver from 'file-saver'
import XLSX from 'xlsx'
import notype from '@/components/utlis/notype.vue'

export default {
  components: { pages,notype },
  name: 'businessdetail',
  props: ['sid'],
  data() {
    return {
      show: false,
      curid: 0,
      businessledgercssum:{
        id:this.sid*1,
        date:0,
        incoinsum:0,
        outcoinsum:0,
        surplus:0,
        remark:''
      },
      // 表格展示数据
      tableData: [],
      // 缓存数据
      temdata: {
        id: 0,
        date: '',
        buyer: '',
        incoin: 0,
        outcoin: 0,
        surplus: 0,
        remark: '',
        sid:0
      },
    }
  },
  methods: {
    // 关闭添加弹窗
    showoff() {
      this.show = false
    },
    // 添加栏目数据
    add() {
      if (this.temdata.date != '') {
        // 数据格式化
        this.temdata.incoin = this.temdata.incoin * 1
        this.temdata.outcoin = this.temdata.outcoin * 1
        if(this.temdata.incoin < 0){
          this.temdata.outcoin = this.temdata.incoin
          this.temdata.incoin = 0
          }
        if(this.tableData.length > 0) {
          this.temdata.surplus = this.tableData[this.tableData.length-1].surplus + this.temdata.incoin + this.temdata.outcoin
        } else {
          if(this.temdata.incoin !=0 ){
            this.temdata.surplus = this.temdata.incoin
          } else {
            this.temdata.surplus = 0 - this.temdata.outcoin
          }
        }
        this.temdata.date = this.temdata.date.getTime()
        this.temdata.sid = this.sid*1
        this.AddLedgercs(this.temdata)
        this.temdata =  {
        id: 0,
        date: '',
        buyer: '',
        incoin: 0,
        outcoin: 0,
        surplus: 0,
        remark: '',
        sid:0
      }
      }
    },
    back() {
      this.$router.go(-1)
    },
    // 保存订单
    save() {
      this.UpdateSum(this.businessledgercssum)
    },
    // 获取选中行
    handleSelectionChange(val) {
      this.multipleSelection = val;
      console.log(val)
      if (val.length > 0) {
        this.curid = val[0].id
      } else {
        this.curid = 0
      }

    },
    // 求和
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = '合计';
          return;
        }
        const values = data.map(item => Number(item[column.property]));
        if (!values.every(value => isNaN(value))) {
          if (index === 3|| index === 4) {
            sums[index] = values.reduce((prev, curr) => {
              const value = Number(curr);
              if (!isNaN(value)) {
                return prev + curr;
              } else {
                return prev;
              }
            }, 0);
          } else if(index === 5) {
            sums[index] = sums[index-1] + sums[index-2]
          }
        } else {
          sums[index] = '';
        }
      });
      this.businessledgercssum.incoinsum=sums[3]
      this.businessledgercssum.outcoinsum=sums[4]
      this.businessledgercssum.surplus=sums[5]
      return sums;
    },
    // 获取所有栏目
    async GetAllLedgercs(sid) {
      const { data: res } = await businessledgerApi.GetAllLedgercs(sid)
      this.tableData = res
    },
    // 添加栏目
    async AddLedgercs(temdata) {
      await businessledgerApi.AddLedgercs(temdata)
      this.GetAllLedgercs(this.sid)
    },
    // 删除选中行
    async dedeleteSalesOrder(curid) {
        this.$confirm('此操作将永久删除该订单，是否继续？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then( async () => {
        const { data: res } = await businessledgerApi.DelLedgercs(curid)
        if (res == 1) {
          this.$message({
            message: '删除成功',
            type: 'success'
          })
          this.GetAllLedgercs(this.sid)
        } else {
          this.$message({
            message: '删除失败',
            type: 'success'
          })
        }
      }).catch(()=>{
          this.$message({
            message: '已取消删除',
            type: 'info'
          })
      })
    },
    // 保存业务订单
    async UpdateSum(businessledgercssum) {
      const { data: res } = await businessledgerApi.UpdateSum(businessledgercssum)
      if (res !== 1) {
        this.$message({
          message: '保存成功',
          type: 'success',
          customClass: 'message',
          center: true,
          duration: 1500
        })
      } else  {
        this.$message({
          message: '保存失败!',
          type: 'error',
          customClass: 'message',
          center: true,
          duration: 1500
        })
      }
    },
    // 导出Excel表格
    exportExcel() {
      var wb = XLSX.utils.table_to_book(document.querySelector("#sales"))
      var wbout = XLSX.write(wb,{
        bookType: 'xlsx',
        bookSST: true,
        type: 'array'
      })
      try {
        FileSaver.saveAs(
          new Blob([wbout],{ type: "application/octet-stream" }),
          "某月业务发生台账详情.xlsx"
        )
      } catch (e) {
        if (typeof console !== 'undefined') console.log(e,wbout)
      }
      return wbout
    } 
  },
  mounted() {
    this.GetAllLedgercs(this.sid)
  }
  ,
  updated() {
    for (let i = 0; i < this.tableData.length; i++) {
      this.$set(
        this.tableData[i], 'date', this.dayjs(this.tableData[i].date).format("YYYY-MM-DD")
      )
    }
  },
}
</script>

<style lang="less" scoped>
.demonstration {
  width: 100px;
  display: inline-block;
}

.inline-block {
  margin-bottom: 5px;
  height: 40px;
}


</style>