const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer:{
    proxy:{
      '/api':{
        target:'http://localhost:53346/',
        changeOrigin:true,// 在本地会创建一个虚拟服务端，然后发送请求的数据，并同时接收请求的数据
        pathRewrite:{ // 路径重写
          '^/api':'' // 替换target中的请求地址
        }
      }
    }
  }
})
