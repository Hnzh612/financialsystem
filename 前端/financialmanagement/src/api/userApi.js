import request from '@/utils/requset'

// 登录
const login = (username,password) => {
    return request.get('admin/login',{
        params: {
            username,
            password
        }
    })
}
// 注册
const register = (user) => {
    return request.post('admin/addadmin',user)
}

export default{
    login,
    register
}
