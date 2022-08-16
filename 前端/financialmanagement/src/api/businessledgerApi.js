import request from "@/utils/requset";

// 获取所有时间业务总和列表
const GetAllSum = (page) => {
    return request.get('businessledgercs/getallsum?page='+page)
}

// 新建某月业务总和
const AddSum = (businessledgercssum) => {
    return request.post('businessledgercs/addsum',businessledgercssum)
}

// 删除当月业务总和以及所连接的栏目
const DelSum = (sid) => {
    return request.get('businessledgercs/delsum?sid=' + sid)
}

// 更新当月业务总和
const UpdateSum = (businessledgercssum) => {
    return request.post('businessledgercs/updatesum',businessledgercssum)
}

// 根据sid获取相应的业务发生栏目
const GetAllLedgercs = (sid) => {
    return request.get('Businessledgercs/GetAllLedgercsBySid?sid=' + sid)
}

// 添加业务发生栏目
const AddLedgercs = (businessledgercs) => {
    return request.post('Businessledgercs/addledgercs',businessledgercs)
}

// 删除业务发生栏目
const DelLedgercs = (id) => {
    return request.get('Businessledgercs/delledgercs?id=' + id)
}
export default {
    GetAllSum,
    AddSum,
    DelSum,
    UpdateSum,
    GetAllLedgercs,
    AddLedgercs,
    DelLedgercs
}