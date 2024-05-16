package com.example.server.Service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Transaction;
import com.example.server.Repository.TransactionRepository;

@Service
@Transactional
public class TransactionService {
    
    @Autowired
    TransactionRepository transactionRepository;

    public Transaction getSingleTransaction(String theId) throws DataNotExistException{
        Transaction transactionSearch = transactionRepository.findTransactionById(theId);
        if(transactionSearch == null){
            throw new DataNotExistException("Transaction is not present");
        }
        return transactionSearch;
    }

    public Transaction addTransaction(Transaction transaction){

    }


    
}
