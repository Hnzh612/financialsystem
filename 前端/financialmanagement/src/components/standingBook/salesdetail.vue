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
          <span class="demonstration">品&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;种:</span>
          <el-select v-model="temdata.variety" placeholder="请选择" style="width:180px">
            <el-option v-for="item in varietys" :key="item.id" :label="item.name" :value="item.name">
            </el-option>
          </el-select>
        </div>
        <div class="inline-block">
          <span class="demonstration">材&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;质：</span>
          <el-select v-model="temdata.Material" placeholder="请选择" style="width:180px">
            <el-option v-for="item in Materials" :key="item.id" :label="item.name" :value="item.name">
            </el-option>
          </el-select>
        </div>
        <div class="inline-block">
          <span class="demonstration">产&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;地：</span>
          <el-select v-model="temdata.city" placeholder="请选择" style="width:180px">
            <el-option v-for="item in cities" :key="item.id" :label="item.name" :value="item.name">
            </el-option>
          </el-select>
        </div>
        <div class="inline-block">
          <span class="demonstration">规&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;格：</span>
          <el-input v-model="temdata.specifications" style="width:180px"></el-input>
        </div>
        <div class="inline-block">
          <span class="demonstration">订货重量：</span>
          <el-input v-model="temdata.weight" style="width:180px"></el-input>
        </div>
        <div class="inline-block">
          <span class="demonstration">销售单价：</span>
          <el-input v-model="temdata.unitprice" style="width:180px" @keyup.enter.native="showoff"></el-input>
        </div>
        <div style="text-align:center;">
          <el-button type="success" circle @click="showoff()">确定</el-button>
        </div>

        <el-button type="success" icon="el-icon-plus" circle slot="reference">添加</el-button>
      </el-popover>
      <el-button type="info" icon="el-icon-arrow-left" circle @click="back()">返回</el-button>
    </div>
    <el-table :data="tableData" style="width: 100%;" :summary-method="getSummaries" show-summary tooltip-effect="dark"
      @selection-change="handleSelectionChange" id="sales">
      <el-table-column type="selection" width="55">
      </el-table-column>
      <el-table-column label="遵义市鑫恒佳耀贸易有限公司对账单" align="center">
        <el-table-column prop="date" label="日期" width="150" align="center">
        </el-table-column>
        <el-table-column prop="variety" label="品种" width="100" align="center">
        </el-table-column>
        <el-table-column prop="material" label="材质" width="100" align="center">
        </el-table-column>
        <el-table-column prop="city" label="产地" width="100" align="center">
        </el-table-column>
        <el-table-column prop="specifications" label="规格" width="120" align="center">
        </el-table-column>
        <el-table-column prop="weight" label="订货重量（吨）" width="90" align="center">
        </el-table-column>
        <el-table-column prop="unitprice" label="销售单价" width="120" align="center">
        </el-table-column>
        <el-table-column prop="amount" label="销售金额" width="120" align="center">
        </el-table-column>
      </el-table-column>
    </el-table>
    <div class="footer">
      <div class="line">
        <div class="row">
          <i>需方（签章）：</i>
          <el-input v-model="companyConnect.rcompany" class="input"></el-input>
        </div>
        <div class="row">
          <i>供方（签章）：</i>
          <el-input v-model="companyConnect.scompany" class="input"></el-input>
        </div>
      </div>
      <div class="line">
        <div class="row">
          <i>经办人：</i>
          <el-input v-model="companyConnect.rname" class="input"></el-input>
        </div>
        <div class="row">
          <i>经办人：</i>
          <el-input v-model="companyConnect.sname" class="input"></el-input>
        </div>
      </div>
      <div class="line">
        <div class="row">
          <i>电话：</i>
          <el-input v-model="companyConnect.rphone" class="input"></el-input>
        </div>
        <div class="row">
          <i>电话：</i>
          <el-input v-model="companyConnect.sphone" class="input"></el-input>
        </div>
      </div>
      <div class="line">
        <div class="row">
          <i>传真：</i>
          <el-input v-model="companyConnect.rfax" class="input"></el-input>
        </div>
        <div class="row">
          <i>传真：</i>
          <el-input v-model="companyConnect.sfax" class="input"></el-input>
        </div>
      </div>
    </div>
  </div>
  <notype v-else></notype>
</template>


<script>
import pages from '@/components/utlis/pages.vue'
import salesApi from '@/api/salesApi'
import paramsApi from '@/api/paramsApi'
import FileSaver from 'file-saver'
import XLSX from 'xlsx'
import notype from '@/components/utlis/notype.vue'

export default {
  components: { pages,notype },
  name: 'salesdetail',
  props: ['cid'],
  data() {
    return {
      show: false,
      curid: 0,
      varietys: [], // 品种
      Materials: [], // 材质
      cities: [], // 
      // 表格展示数据
      tableData: [],
      // 双方公司信息 通过请求获取
      companyConnect: {
        id: 0,
        sname: '',
        rphone: '',
        scompany: '',
        sfax: '',
        rname: '',
        rphone: '',
        rcompany: '',
        rfax: ''
      },
      // 缓存数据
      temdata: {
        id: 0,
        date: '',
        variety: '',
        Material: '',
        city: '',
        specifications: '',
        weight: '',
        unitprice: '',
        amount: ''
      },
      // 查询体
      querybody: {
        rid:0,
        page:1,
        sname: "",
        scompany: "",
        rname: "",
        rcompany: ""
      },
    }
  },
  methods: {
    // 关闭添加弹窗
    showoff() {
      this.show = false
    },
    // 添加数据
    add() {
      if (this.temdata.date != '') {
        // 数据格式化
        this.temdata.cid = this.cid
        this.temdata.unitprice = this.temdata.unitprice * 1
        this.temdata.weight = this.temdata.weight * 1
        this.temdata.amount = this.temdata.unitprice * this.temdata.weight * 1
        this.temdata.date = this.temdata.date.getTime()
        this.addSalesOrder(this.temdata)
        this.temdata = {}
      }
    },
    back() {
      this.$router.go(-1)
    },
    // 保存订单
    save() {
      this.companyConnect.date = this.dayjs(this.companyConnect.date).$d.getTime()
      this.UpdateSalesConnect(this.companyConnect)
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
          if (index === 6  || index === 8) {
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
      // 格式化数据
      sums[6] = sums[6].toFixed(3)
      sums[8] = sums[8].toFixed(2)
      return sums;
    },
    // 获取所有栏目
    async getAllSalesOrder(cid) {
      const { data: res } = await salesApi.getAllSalesOrder(cid)
      this.tableData = res
    },
    // 获取双方信息
    async SelectSalesConnect(querybody) {
      querybody.rid = this.cid
      const { data: res } = await salesApi.getAllSalesConnect(querybody)
      this.$set(
        res.data[0], 'date', this.dayjs(res.data[0].date).format('YYYY-MM-DD')
      )
      this.companyConnect = res.data[0]
    },
    // 添加栏目
    async addSalesOrder(SalesOrder) {
      await salesApi.addSalesOrder(SalesOrder)
      this.getAllSalesOrder(this.cid)
    },
    // 删除选中行
    async dedeleteSalesOrder(curid) {
            this.$confirm('此操作将永久删除该订单，是否继续？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then( async () => {
        const { data: res } = await salesApi.deleteSalesOrder(curid)
        if (res == 1) {
          this.$message({
            message: '删除成功',
            type: 'success'
          })
          this.getAllSalesOrder(this.cid)
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
    // 获取销售订单参数
    async getparams() {
      const { data: res } = await paramsApi.getAllParams()
      for(let i =0;i<res.length;i++) {
        if(res[i].type == 0) { this.varietys.push(res[i])}
        if(res[i].type == 1) { this.Materials.push(res[i])}
        if(res[i].type == 2) { this.cities.push(res[i])}
      }
    },
    // 保存销售订单
    async UpdateSalesConnect(companyConnect) {
      const { data: res } = await salesApi.UpdateSalesConnect(companyConnect)
      if (res == 1) {
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
          `${this.tableData[0].date.slice(0,7)}-销售详情.xlsx`
        )
      } catch (e) {
        if (typeof console !== 'undefined') console.log(e,wbout)
      }
      return wbout
    } 
  },
  mounted() {
    this.getAllSalesOrder(this.cid)
    this.SelectSalesConnect(this.querybody)
    this.getparams()
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
</style>