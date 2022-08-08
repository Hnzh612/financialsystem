<template>
    <div class="body">
        <div class="login">
            <div class="title">
                <i>鑫恒佳耀</i>&nbsp;&nbsp;<em>财务管理平台</em>
            </div>
            <div class="main" v-if="logintype == 1">
                <input type="text" class="username" v-model="username" placeholder="请输入用户名">
                <input type="password" class="password" v-model="password" placeholder="请输入密码">
                <input class="btn" type="button" value="立即登录" @click="login">
                <p><i>还未注册，</i><a class="Add" @click="change">点击前往注册</a></p>
            </div>
            <div class="main" :style="logintype==0?'height:320px':''" v-if="logintype == 0">
                <input type="text" class="username" v-model="newUsername" placeholder="请输入用户名">
                <input type="password" class="password" v-model="newPassowrd" placeholder="请输入密码">
                <input type="password" class="password" v-model="againPassowrd" placeholder="请再次输入密码">
                <input class="btn" type="button" value="立即注册" @click="register">
                <p><i>已有账号，</i><a class="Add" @click="change">点击前往登录</a></p>
            </div>
        </div>
    </div>
</template>

<script>
import userApi from '@/api/userApi'

export default {
    name: 'login',
    data() {
        return {
            username: '',
            password: '',
            logintype: 1,
            newUsername:'',
            newPassowrd:'',
            againPassowrd:''
        }
    },
    methods: {
        async login() {
            const { data: res } = await userApi.login(this.username, this.password)
            if (this.password == res[0].password) {
                localStorage.setItem('username',this.username)
                localStorage.setItem('type',res[0].type==0?'用户':'管理员')
                this.$message({
                    message: '恭喜你登录成功',
                    type: 'success',
                    customClass: 'message',
                    offset: 450,
                    center: true,
                    duration: 1500,
                    onClose: () => { this.$router.push('/home') }
                })
            } else {
                this.$message({
                    message: '登录失败，请检查你的用户名或者密码',
                    type: 'error',
                    offset :450,
                    duration: 1500,
                    center:true,
                })
            }

        },
        async register() {
            if(this.newUsername=='' || this.againPassowrd=='' || this.newPassowrd=='') {
                    this.$message({
                    message: '账号或者密码不能为空',
                    type: 'error',
                    offset :550,
                    duration: 1500,
                    center:true,
                })
            } else if(this.newPassowrd !== this.againPassowrd) {
                    this.$message({
                    message: '两次输入的密码不一致',
                    type: 'error',
                    offset :550,
                    duration: 1500,
                    center:true,
                    })
            } else {
                let user = {
                    id:0,
                    username:this.newUsername,
                    password:this.newPassowrd,
                    type:0
                }
                const { data: res1} = await userApi.login(this.newUsername)
                if(res1[0].username == this.newUsername) {
                    this.$message({
                    message: '该用户名已被注册',
                    type: 'error',
                    offset :550,
                    duration: 1500,
                    center:true,
                    })
                }
                else {
                    const { data: res } = await userApi.register(user)
                    if (res == 1) {
                        this.$message({
                        message: '恭喜你注册成功',
                        type: 'success',
                        customClass: 'message',
                        offset: 450,
                        center: true,
                        duration: 1500,
                        onClose: () => { this.logintype = 1 }
                        })                        
                    }
                }
            }
        },
        change() {
            this.logintype = this.logintype == 1?0:1
        }
    },
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

            em,
            i {
                font-size: 25px;
            }

            i {
                color: rgb(0, 255, 255);
            }

            em {
                color: aliceblue;
            }
        }

        .main {
            text-align: center;
            height: 250px;
            padding-top: 30px;
            border-radius: 0 0 10px 10px;
            background-color: #FFF;

            a {
                cursor: pointer;
            }
            input {
                outline: none;
                width: 80%;
                height: 40px;
                margin-bottom: 30px;
                border: 0;
                border-bottom: 1px solid #dcdcdc;
                font-size: 15px;
            }

            .btn {
                margin-bottom: 10px;
                cursor: pointer;
            }

            .Add {
                color: #006685;
            }
        }
    }
}

.message {
    height: 200px;
}
</style>