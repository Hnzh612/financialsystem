import axios from 'axios'

const request = axios.create({
    // 指定请求的根路径
    baseURL: '/api',
    // 设置超时时间
    timeout:10000
})
// 公共配置的post的Content-type
request.defaults.headers.post['Content-type'] = 'application/json;charset=UTF-8'
// 自动给同一个vue项目的所有请求添加请求投
request.interceptors.request.use(function (config) {
    let token = localStorage.getItem('token')
    if(token) {
        config.headers['Authorization'] = token
    }
    return config
  })
  
export default request