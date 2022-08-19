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
      @selection-change="handleSelectionChange" id="invoicing">
      <el-table-column type="selection" width="55">
      </el-table-column>
      <el-table-column label="遵义市鑫恒佳耀贸易有限公司进销存管理表" align="center">
        <el-table-column prop="date" label="日期" width="150" align="center">
        </el-table-column>
        <el-table-column prop="bcompany" label="采购单位" align="center">
        </el-table-column>
        <el-table-column prop="variety" label="商品品名" width="110" align="center">
        </el-table-column>
        <el-table-column prop="material" label="规格型号" width="110" align="center">
        </el-table-column>
        <el-table-column prop="city" label="产地" width="110" align="center">
        </el-table-column>
        <el-table-column prop="specifications" label="规格" width="110" align="center">
        </el-table-column>
        <el-table-column label="入库">
          <el-table-column prop="inweight" label="入库数量/吨" width="110" align="center">
          </el-table-column>
          <el-table-column prop="inunitprice" label="单价" width="110" align="center">
          </el-table-column>
          <el-table-column prop="inamount" label="金额" width="110" align="center">
          </el-table-column>
        </el-table-column>
        <el-table-column label="出库">
          <el-table-column prop="outweight" label="出库数量/吨" width="110" align="center">
          </el-table-column>
          <el-table-column prop="outunitprice" label="单价" width="110" align="center">
          </el-table-column>
          <el-table-column prop="outamount" label="金额" width="110" align="center">
          </el-table-column>
        </el-table-column>
        <el-table-column label="库存">
          <el-table-column prop="leftweight" label="结存数量" width="110" align="center">
          </el-table-column>
          <el-table-column prop="leftamount" label="结存金额" width="110" align="center">
          </el-table-column>
        </el-table-column>
      </el-table-column>
    </el-table>
  </div>
  <notype v-else></notype>
</template>


<script>
import pages from '@/components/utlis/pages.vue'
import invoicingApi from '@/api/invoicingApi'
import FileSaver from 'file-saver'
import XLSX from 'xlsx'
import notype from '@/components/utlis/notype.vue'

export default {
  components: { pages,notype },
  name: 'invoicingdetail',
  props: ['sid'],
  data() {
    return {
      show: false,
      curid: 0,
      invoicing: {
        id: this.sid * 1,
        date: 0,
        inweightsum: 0,
        inamountsum: 0,
        outweightsum: 0,
        outamountsum: 0,
        leftweightsum: 0,
        leftamountsum: 0,
        remark: '',
      },
      // 表格展示数据
      tableData: [],
      // 缓存数据
      temdata: {
        id: 0,
        date: 0,
        bcompany: 0,
        variety: 0,
        material: 0,
        city: 0,
        specifications: 0,
        inweight: 0,
        inunitprice: 0,
        inamount: 0,
        outweight: 0,
        outunitprice: 0,
        outamount: 0,
        leftweight: 0,
        leftamount: 0,
        sid: 0
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
        this.temdata.inweightsum = this.temdata.inweightsum * 1
        this.temdata.inunitprice = this.temdata.inunitprice * 1
        this.temdata.inamountsum = this.temdata.inweightsum * this.tableData.inunitprice
        this.temdata.outweightsum = this.temdata.outweightsum * 1
        this.temdata.outunitprice = this.temdata.outunitprice * 1
        this.temdata.outamountsum = this.temdata.outweightsum * this.temdata.outunitprice
        this.temdata.leftweightsum = this.temdata.inweightsum - this.temdata.outweightsum
        this.temdata.leftamountsum = this.temdata.outamountsum - this.temdata.outamountsum
        this.temdata.date = this.temdata.date.getTime()
        this.temdata.sid = this.sid * 1
        this.AddLedgercs(this.temdata)
        this.temdata = {
          id: 0,
          date: 0,
          bcompany: 0,
          variety: 0,
          material: 0,
          city: 0,
          specifications: 0,
          inweight: 0,
          inunitprice: 0,
          inamount: 0,
          outweight: 0,
          outunitprice: 0,
          outamount: 0,
          leftweight: 0,
          leftamount: 0,
          sid: 0
        }
      }
    },
    back() {
      this.$router.go(-1)
    },
    // 保存订单
    save() {
      this.UpdateSum(this.invoicing)
    },
    // 获取选中行
    handleSelectionChange(val) {
      this.multipleSelection = val;
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
          if (index === 7 || index === 9 || index == 10 || index == 12 || index == 13 || index == 14) {
            sums[index] = values.reduce((prev, curr) => {
              const value = Number(curr);
              if (!isNaN(value)) {
                return prev + curr;
              } else {
                return prev;
              }
            }, 0);
          }
        } else {
          sums[index] = '';
        }
      });
      this.invoicing.inweightsum = sums[7]
      this.invoicing.inamountsum = sums[9]
      this.invoicing.outweightsum = sums[10]
      this.invoicing.outamountsum = sums[12]
      this.invoicing.leftweightsum = sums[13]
      this.invoicing.leftamountsum = sums[14]
      return sums;
    },
    // 获取所有栏目
    async GetAllLedgercs(sid) {
      const { data: res } = await invoicingApi.GetAllLedgercs(sid)
      this.tableData = res
    },
    // 添加栏目
    async AddLedgercs(temdata) {
      await invoicingApi.AddLedgercs(temdata)
      this.GetAllLedgercs(this.sid)
    },
    // 删除选中行
    async dedeleteSalesOrder(curid) {
      this.$confirm('此操作将永久删除该订单，是否继续？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        const { data: res } = await invoicingApi.DelLedgercs(curid)
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
      }).catch(() => {
        this.$message({
          message: '已取消删除',
          type: 'info'
        })
      })
    },
    // 保存业务订单
    async UpdateSum(invoicing) {
      const { data: res } = await invoicingApi.UpdateSum(invoicing)
      if (res !== 1) {
        this.$message({
          message: '保存成功',
          type: 'success',
          customClass: 'message',
          center: true,
          duration: 1500
        })
      } else {
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
      var wb = XLSX.utils.table_to_book(document.querySelector("#invoicing"))
      var wbout = XLSX.write(wb,{
        bookType: 'xlsx',
        bookSST: true,
        type: 'array'
      })
      try {
        FileSaver.saveAs(
          new Blob([wbout],{ type: "application/octet-stream" }),
          "某月进销存详情.xlsx"
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