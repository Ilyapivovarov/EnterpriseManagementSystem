export interface UpdatedTask {
    id: number,
    guid: string,
    name: string,
    description?: string,
    created: Date,
    authorId: number
    executorId?: number,
    statusId: number
    inspector?: number,
}
