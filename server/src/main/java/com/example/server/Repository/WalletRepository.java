package com.example.server.Repository;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import com.example.server.Models.Wallet;

@Repository
public interface WalletRepository extends MongoRepository<Wallet, String> {

    // List<Wallet> findAll();

    Wallet findWalletById(String theId);
}
