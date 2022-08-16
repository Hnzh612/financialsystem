import request from "@/utils/requset"

// 获取全部销售参数
const getAllParams = () => {
    return request.get('param/getAllParams')
}

// 添加参数
const addParam = (param) => {
    return request.post('param/addParam', param)
}

// 删除参数
const deleteParamById = (id) => {
    return request.get('param/deleteParamById?id=' + id)
}

export default {
    getAllParams,
    addParam,
    deleteParamById,
}