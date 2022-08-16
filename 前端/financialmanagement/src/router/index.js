import Vue from 'vue'
import VueRouter from 'vue-router'

import myLogin from '@/components/myLogin.vue'
import myHome from '@/components/myHome.vue'
// 导入台账组件
import InvoicingLedger from '@/components/standingBook/InvoicingLedger.vue'
import salesLedger from '@/components/standingBook/salesLedger.vue'
import salesdetail from '@/components/standingBook/salesdetail.vue'
import businessledger from '@/components/standingBook/businessledger.vue'
import businessdetail from '@/components/standingBook/businessdetail.vue'
import invoicingdetail from '@/components/standingBook/invoicingdetail.vue'
// 导入设置组件
import salesparam from '@/components/settings/salesparam.vue'


Vue.use(VueRouter)

const routes = [
  { path:'/',redirect: "/login"},
  {
    path: '/login',
    component: myLogin
  },
  { path: '/home', component: myHome, redirect: '/home/sales',
    children:[
      { path:'invoicing', component: InvoicingLedger },
      { path:'invoicingdetail/:sid', component:invoicingdetail, props:true },
      { path:'business', component:businessledger },
      { path:'businessdetail/:sid', component:businessdetail, props:true },
      { path:'sales', component: salesLedger },
      { path:'salesdetail/:cid', component: salesdetail , props:true }
      
    ]  
  },
  { path: '/settings', component: myHome, redirect: '/settings/salesparam',
    children:[
      { path:'salesparam', component: salesparam }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router
