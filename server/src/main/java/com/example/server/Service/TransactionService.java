package com.example.server.Service;

import org.apache.el.stream.Optional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Transaction;
import com.example.server.Models.User;
import com.example.server.Repository.TransactionRepository;
import com.example.server.Repository.UserRepository;
import com.example.server.Repository.WalletRepository;
import java.util.Date;
import java.util.List;
import com.example.server.DTO.DataResponse;
@Service
@Transactional
public class TransactionService {
    
    @Autowired
    TransactionRepository transactionRepository;

    @Autowired
    WalletRepository walletRepository;

    @Autowired
    UserRepository userRepository;


    public Transaction getSingleTransaction(String theId) throws DataNotExistException{
        Transaction transactionSearch = transactionRepository.findTransactionById(theId);
        if(transactionSearch == null){
            throw new DataNotExistException("Transaction is not present");
        } 
        return transactionSearch;
    }

    public Transaction addTransaction(Transaction transaction){
        Transaction saved = transactionRepository.save(transaction);
        // System.err.println(saved);
        User userSearch = userRepository.findUserById(saved.getUserId());
        System.err.println(userSearch);
        return saved;
    }


    public Transaction updateTransaction(Transaction transaction){
        Optional<Transaction> existingTransaction = transactionRepository.findTransactionById(transaction.getId());
        if(!existingTransaction.isPresent()) {
            throw new DataNotExistException("Transaction is not present");
        }
        return transactionRepository.save(transaction);
    }

    public List<Transaction> searchTransactionUserDate(String userId, String date){
        return transactionRepository.findTransactionsByUserIdAndDate(userId, date);
    }


    public List<Transaction> findAllTransactionByUser(String email){
        User user = userRepository.findByEmail(email);
        if(user == null) {
            throw new DataNotExistException("User not found");
        }
        return transactionRepository.findTransactionsByUserId(user.getId());
    }

    


    
}
