package com.example.server.Repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.Transaction;

@Repository
public interface TransactionRepository extends MongoRepository<Transaction, String>{
    Optional<Transaction> findTransactionById(String theId);
    List<Transaction> findTransactionsByUserId(String theId);
    List<Transaction> findTransactionsByUserIdAndDate(String userId, String date);
}
