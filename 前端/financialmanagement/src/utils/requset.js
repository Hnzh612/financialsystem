import axios from 'axios'

const request = axios.create({
    // 指定请求的根路径
    baseURL: '/api',
    // 设置超时时间
    timeout:10000
})
// 公共配置的post的Content-type
request.defaults.headers.post['Content-type'] = 'application/json;charset=UTF-8'

export default request