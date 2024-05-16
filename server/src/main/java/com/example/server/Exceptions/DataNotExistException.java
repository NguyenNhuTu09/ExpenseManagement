package com.example.server.Exceptions;

public class DataNotExistException extends IllegalArgumentException {

    public DataNotExistException(String msg) {
        super(msg);
    }
}
