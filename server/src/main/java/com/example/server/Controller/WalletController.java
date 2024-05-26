package com.example.server.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.server.Models.Transaction;
import com.example.server.Models.Wallet;
import com.example.server.Service.WalletService;

@RequestMapping("/wallets")
@RestController
public class WalletController {
    
    @Autowired
    WalletService walletService;

    @GetMapping("/{id}")
    public ResponseEntity<Wallet> getWallet(@PathVariable String id) {
        try {
            Wallet wallet = walletService.getSingleWallet(id);
            return ResponseEntity.ok(wallet);
        } catch (Exception e) {
            return ResponseEntity.status(404).body(null);
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<Wallet> updateWallet(@PathVariable String id, @RequestBody Transaction transaction) {
        try {
            transaction.setUserId(id);
            Wallet updatedWallet = walletService.updateWallet(transaction);
            return ResponseEntity.ok(updatedWallet);
        } catch (Exception e) {
            return ResponseEntity.status(400).body(null);
        }
    }
}
