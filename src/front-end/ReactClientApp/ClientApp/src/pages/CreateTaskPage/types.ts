export interface CreateTaskModel {
    name?: string,
    description?: string,
    authorGuid: string,
    statusId?: number,
    executorId?: number,
    inspectorId?: number,
}
