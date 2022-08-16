<template>
    <el-row class="tac">
        <el-col :span="12">
            <el-menu default-active="1" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">
                <el-submenu index="1">
                    <template slot="title">
                        <i class="el-icon-coin"></i>
                        <span>公司台账</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item>
                            <router-link to="/home/sales">销售台账</router-link>
                        </el-menu-item>
                        <el-menu-item>
                            <router-link to="/home/invoicing">进销存管理表</router-link>
                        </el-menu-item>
                        <el-menu-item>
                            <router-link to="/home/business">业务发生台账</router-link>
                        </el-menu-item>
                    </el-menu-item-group>
                </el-submenu>
                <el-menu-item index="2">
                    <i class="el-icon-menu"></i>
                    <span slot="title">导航二</span>
                </el-menu-item>
                <el-menu-item index="3" disabled>
                    <i class="el-icon-document"></i>
                    <span slot="title">导航三</span>
                </el-menu-item>
                <el-submenu index="4">
                    <template slot="title">
                        <i class="el-icon-setting"></i>
                        <span>系统设置</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item>
                            <router-link to="/settings/salesparam">销售参数</router-link>
                        </el-menu-item>
                        <el-menu-item>
                            <router-link to="/home/purchase">其他参数</router-link>
                        </el-menu-item>
                    </el-menu-item-group>
                </el-submenu>
                <el-menu-item index="5" disabled>
                    <i class="el-icon-document"></i>
                    <span slot="title">导航五</span>
                </el-menu-item>
            </el-menu>
        </el-col>
    </el-row>
</template>

<script>
export default {
    name: 'myAside',
    methods: {
        handleOpen(key, keyPath) {
            //console.log(key, keyPath);
        },
        handleClose(key, keyPath) {
            //console.log(key, keyPath);
        },
        goItem(e) {
            //console.log(e.index)
        }
    }
}
</script>

<style lang="less" scoped>
.el-submenu .el-menu-item {
    min-width: 150px !important;
}

.el-menu {
    border: 0;
}

.el-col-12 {
    width: 100%;
}

.el-row {
    border-right: solid 1px #e6e6e6;
}

.el-menu-item a:hover {
    color: #409EFF;
}
</style>