import request from '@/utils/requset'

// 根据条件查询订单
const getAllSalesConnect = (querybody) => {
    return request.get('sales/getAllSalesConnect',{
        params:{ 
            page:querybody.page,
            sname:querybody.sname,
            scompany:querybody.scompany,
            rname:querybody.rname,
            rcompany:querybody.rcompany,
            rid:querybody.rid
        }
    })
}
// 根据id删除订单
const deleteSalesConnectById = (id) => {
    return request.get('sales/deleteSalesConnectById?id=' + id)
}
// 添加订单
const addSalesConnect = (SalesConnect) => {
    return request.post('sales/addSalesConnect', SalesConnect)
}
// 更新订单
const UpdateSalesConnect = (SalesConnect) => {
    return request.post('sales/UpdateSalesConnect', SalesConnect)
}
// 获取所有栏目
const getAllSalesOrder = (cid) => {
    return request.get('sales/SelectSalesOrders?cid='+cid)
}
// 添加栏目
const addSalesOrder = (SalesOrder) => {
    return request.post('sales/addSalesOrder', SalesOrder)
}
// 删除栏目
const deleteSalesOrder = (id) => {
    return request.get('sales/deleteSalesOrder?id=' + id)
}


export default {
    getAllSalesConnect,
    deleteSalesConnectById,
    addSalesConnect,
    UpdateSalesConnect,
    getAllSalesOrder,
    addSalesOrder,
    deleteSalesOrder
}
