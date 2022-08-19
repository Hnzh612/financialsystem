<template>
    <div v-if="this.$type == '管理员'">
        <div class="box">
            <span>品种：</span>
            <el-tag :key="tag.id" v-for="tag in salesparams.varietys" closable :disable-transitions="false"
                @close="handleClose(tag)">
                {{ tag.name }}
            </el-tag>
            <el-input class="input-new-tag" v-if="show.varietysinputVisible" v-model="inputValue" ref="saveTagInput"
                size="small" @keyup.enter.native="varietyshandleInputConfirm" @blur="varietyshandleInputConfirm">
            </el-input>
            <el-button v-else class="button-new-tag" size="small" @click="showInput(salesparams.varietys[0].type)">+ 新品种
            </el-button>
        </div>
        <div class="box">
            <span>材质：</span>
            <el-tag :key="tag.id" v-for="tag in salesparams.Materials" closable :disable-transitions="false"
                @close="handleClose(tag)">
                {{ tag.name }}
            </el-tag>
            <el-input class="input-new-tag" v-if="show.MaterialsinputVisible" v-model="inputValue" ref="saveTagInput"
                size="small" @keyup.enter.native="MaterialshandleInputConfirm" @blur="MaterialshandleInputConfirm">
            </el-input>
            <el-button v-else class="button-new-tag" size="small" @click="showInput(salesparams.Materials[0].type)">+
                新材质</el-button>
        </div>
        <div class="box">
            <span>产地：</span>
            <el-tag :key="tag.id" v-for="tag in salesparams.cities" closable :disable-transitions="false"
                @close="handleClose(tag)">
                {{ tag.name }}
            </el-tag>
            <el-input class="input-new-tag" v-if="show.citiesinputVisible" v-model="inputValue" ref="saveTagInput"
                size="small" @keyup.enter.native="citieshandleInputConfirm" @blur="citieshandleInputConfirm">
            </el-input>
            <el-button v-else class="button-new-tag" size="small" @click="showInput(salesparams.cities[0].type)">+ 新产地
            </el-button>
        </div>
    </div>
    <notype v-else></notype>

</template>

<script>
import paramsApi from '@/api/paramsApi';
import notype from '@/components/utlis/notype.vue'

export default {
    components:{ notype },
    data() {
        return {
            salesparams: {
                varietys: [],
                Materials: [],
                cities: [],
                change: false
            },
            show: {
                varietysinputVisible: false,
                MaterialsinputVisible: false,
                citiesinputVisible: false,
            },

            inputValue: ''
        };
    },
    methods: {
        // 删除标签
        handleClose(tag) {
            this.delParam(tag.id)
            this.salesparams.change = true
        },
        // 点击时聚焦
        showInput(type) {
            switch (type) {
                case 0: this.show.varietysinputVisible = true; break;
                case 1: this.show.MaterialsinputVisible = true; break;
                case 2: this.show.citiesinputVisible = true; break;
            }
            this.$nextTick(_ => {
                this.$refs.saveTagInput.$refs.input.focus();
            });
        },
        // 结束点击事件（添加品种）
        varietyshandleInputConfirm() {
            let inputValue = {}
            if (this.inputValue != '') {
                inputValue.name = this.inputValue
                inputValue.type = 0
                this.addParam(inputValue)
                this.salesparams.change = true
            }

            this.show.varietysinputVisible = false

            this.inputValue = ''
        },
        // 结束点击事件（添加材质）
        MaterialshandleInputConfirm() {
            let inputValue = {}
            if (this.inputValue != '') {
                inputValue.name = this.inputValue
                inputValue.type = 1
                this.addParam(inputValue)
                this.salesparams.change = true
            }

            this.show.MaterialsinputVisible = false
            this.inputValue = ''
        },
        // 结束点击事件（添加产地）
        citieshandleInputConfirm() {
            let inputValue = {}
            if (this.inputValue != '') {
                inputValue.name = this.inputValue
                inputValue.type = 2
                this.addParam(inputValue)
                this.salesparams.change = true
            }
            this.show.citiesinputVisible = false
            this.inputValue = ''
        },
        // 获取所有销售参数
        async getAllParams() {
            const { data: res } = await paramsApi.getAllParams()
            this.salesparams.varietys = []
            this.salesparams.Materials = []
            this.salesparams.cities = []
            for (let i = 0; i < res.length; i++) {
                if (res[i].type == 0) { this.salesparams.varietys.push(res[i]) }
                if (res[i].type == 1) { this.salesparams.Materials.push(res[i]) }
                if (res[i].type == 2) { this.salesparams.cities.push(res[i]) }
            }
            this.salesparams.change = false
        },
        // 删除销售参数
        async delParam(id) {
            const { data: res } = await paramsApi.deleteParamById(id)
            if (res == 1) {
                this.$message({
                    type: 'success',
                    message: '删除成功'
                })
            } else {
                this.$message({
                    type: 'error',
                    message: '删除失败'
                })
            }
        },
        // 添加参数
        async addParam(param) {
            await paramsApi.addParam(param)
        }
    },
    created() {
        this.getAllParams()
    },
    watch: {
        'salesparams.change': {
            handler() {
                this.getAllParams()
            }
        },
        deep: true,
        immediate: true
    }
}
</script>

<style lang="less" scoped>
.el-tag+.el-tag {
    margin-left: 10px;
}

.button-new-tag {
    margin-left: 10px;
    height: 32px;
    line-height: 30px;
    padding-top: 0;
    padding-bottom: 0;
}

.input-new-tag {
    width: 90px;
    margin-left: 10px;
    vertical-align: bottom;
}

.box {
    margin: 20px 10px;
    padding-left: 20px;
    border-left: 3px solid #e6e6e6;
}
</style>