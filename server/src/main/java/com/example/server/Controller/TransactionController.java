package com.example.server.Controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.example.server.DTO.DataResponse;
import com.example.server.Exceptions.CustomException;
import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Transaction;
import com.example.server.Service.TransactionService;


@RequestMapping("/transactions")
@RestController
public class TransactionController {
    @Autowired
    TransactionService transactionService;

    @GetMapping("/{id}")
    public ResponseEntity<DataResponse> getTransaction(@PathVariable String id) {
        try {
            Transaction transaction = transactionService.getSingleTransaction(id);
            return new ResponseEntity<>(new DataResponse("sucessfully search", transaction), HttpStatus.OK);
        } catch (DataNotExistException e) {
            return ResponseEntity.status(404).body(null);
        }
    }

    @PostMapping
    public ResponseEntity<DataResponse> addTransaction(@RequestBody Transaction transaction) {
        try {
            Transaction createdTransaction = transactionService.addTransaction(transaction);
            return new ResponseEntity<>(new DataResponse("created sucessfully", createdTransaction), HttpStatus.CREATED);
        } catch (CustomException e) {
            return ResponseEntity.status(400).body(null);
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<DataResponse> updateTransaction(@PathVariable String id, @RequestBody Transaction transaction) {
        try {
            transaction.setId(id);
            Transaction updatedTransaction = transactionService.updateTransaction(transaction);
            return new ResponseEntity<>(new DataResponse("updated sucessfully", updatedTransaction), HttpStatus.OK);
        } catch (DataNotExistException e) {
            return ResponseEntity.status(404).body(null);
        } catch (Exception e) {
            return ResponseEntity.status(400).body(null);
        }
    }

    @GetMapping("/search")
    public ResponseEntity<List<Transaction>> searchTransactions(@RequestParam String userId, @RequestParam String date) {
        try {
            List<Transaction> transactions = transactionService.searchTransactionUserDate(userId, date);
            return ResponseEntity.ok(transactions);
        } catch (Exception e) {
            return ResponseEntity.status(400).body(null);
        }
    }

    @GetMapping("/user/{email}")
    public ResponseEntity<List<Transaction>> getTransactionsByUser(@PathVariable String email) {
        try {
            List<Transaction> transactions = transactionService.findAllTransactionByUser(email);
            return ResponseEntity.ok(transactions);
        } catch (DataNotExistException e) {
            return ResponseEntity.status(404).body(null);
        }
    }
}
