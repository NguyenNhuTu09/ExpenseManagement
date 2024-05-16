package com.example.server.Repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.Transaction;

@Repository
public interface TransactionRepository extends MongoRepository<Transaction, String>{
    Transaction findTransactionById(String theId);
}
