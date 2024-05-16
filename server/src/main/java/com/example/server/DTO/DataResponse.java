package com.example.server.DTO;

public class DataResponse {
    private Boolean success;
    private String message;
    private int count;
    private Object data;
    public DataResponse(){
    }
    public DataResponse(String message, Object data) {
        this.message = message;
        this.data = data;
    }

    public DataResponse(String message, int count, Object data){
        this.message = message;
        this.count = count;
        this.data = data;
    }
    public DataResponse(Boolean success, String message){
        this.success = success;
        this.message = message;
    }
    public DataResponse(int count, Object data){
        this.count = count;
        this.data = data;
    }
    public String getMessage() {
        return message;
    }
    public void setMessage(String message) {
        this.message = message;
    }
    public Object getData() {
        return data;
    }
    public void setData(Object data) {
        this.data = data;
    }

    public void setCount(int count) {
        this.count = count;
    }
    public int getCount() {
        return count;
    }

    public Boolean getSuccess() {
        return success;
    }
    public void setSuccess(Boolean success) {
        this.success = success;
    }
}
