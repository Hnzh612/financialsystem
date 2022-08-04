<template>
    <div class="body">
        <div class="login">
            <div class="title">
                <i>鑫恒佳耀</i>&nbsp;&nbsp;<em>财务管理平台</em>
            </div>
            <div class="main">
                <input type="text" class="username" placeholder="请输入用户名">
                <input type="password" class="password" placeholder="请输入密码">
                <input type="button" value="立即登录" @click="login">
            </div>
        </div>
    </div>
</template>

<script>
export default {
  name: 'login',
    data() {
        return {
            username: '',
            password: ''
        }
    },
    methods: {
        login() {
            this.$message({
                message: '恭喜你登录成功',
                type: 'success',
                customClass: 'message',
                offset: 450,
                center: true,
                duration: 1500,
                onClose: ()=> { this.$router.push('/home')}
            })
        }
    }
}
</script>

<style lang="less" scoped>
.body {
    height: 100%;
    background: repeat url(@/assets/background.jpg);
    display: flex;
    justify-content: center;
    .login {
        width: 400px;
        height: 300px;
        border-radius: 20px;
        margin-top: 200px;
        .title {
            height: 50px;
            border-radius: 10px 10px 0 0;
            background-color: rgba(0, 1, 2, 0.7);
            text-align: center;
            line-height: 50px;
            em,i {font-size: 25px;}
            i { color: rgb(0, 255, 255);}
            em { color: aliceblue;}
        }
        .main {
            text-align: center;
            height: 250px;
            padding-top: 30px;
            border-radius: 0 0 10px 10px;
            background-color: #FFF;
            input {
                outline: none;
                width: 80%;
                height: 40px;
                margin-bottom: 30px;
                border: 0;
                border-bottom: 1px solid #dcdcdc ;
                font-size: 15px;
            }
        }
    }
}
.message {
    height: 200px;
}
</style>