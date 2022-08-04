import Vue from 'vue'
import App from './App.vue'
import router from './router'
// 导入element-ui组件库
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
// 导入样式
import '@/css/index.css'

Vue.config.productionTip = false
Vue.use(ElementUI)
Vue.directive('title', {
  inserted: function (el, binding) {
    document.title = el.dataset.title
  }
})
new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
