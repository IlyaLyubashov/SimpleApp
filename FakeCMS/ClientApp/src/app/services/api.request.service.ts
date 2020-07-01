

export class RequestApiService{
    protected readonly request_url : string;


    constructor(additionalPathPart?: string) {
        this.request_url = `${window.location.origin}/api`
        if(additionalPathPart)
            this.request_url += `/${additionalPathPart}`;
    }
}