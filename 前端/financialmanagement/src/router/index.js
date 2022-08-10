import Vue from 'vue'
import VueRouter from 'vue-router'

import myLogin from '@/components/myLogin.vue'
import myHome from '@/components/myHome.vue'
// 导入子组件
import purchaseLedger from '@/components/standingBook/purchaseLedger.vue'
import salesLedger from '@/components/standingBook/salesLedger.vue'
import salesdetail from '@/components/standingBook/salesdetail.vue'


Vue.use(VueRouter)

const routes = [
  { path:'/',redirect: "/login"},
  {
    path: '/login',
    component: myLogin
  },
  { path: '/home', component: myHome, 
    children:[
      { path:'purchase', component: purchaseLedger },
      { path:'sales', component: salesLedger },
      { path:'salesdetail/:cid', component: salesdetail , props:true}
    ]  
  }
]

const router = new VueRouter({
  routes
})

export default router
