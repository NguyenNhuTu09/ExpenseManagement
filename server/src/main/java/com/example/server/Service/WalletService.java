package com.example.server.Service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.example.server.Enums.TransactionType;
import com.example.server.Exceptions.CustomException;
import com.example.server.Exceptions.DataNotExistException;
import com.example.server.Models.Transaction;
import com.example.server.Models.User;
import com.example.server.Models.Wallet;
import com.example.server.Repository.UserRepository;
import  com.example.server.Repository.WalletRepository;
import com.example.server.Util.Helper;

@Service
@Transactional
public class WalletService {
    
    @Autowired
    WalletRepository walletRepository;

    @Autowired
    UserRepository userRepository;

    public Wallet updateWallet(Transaction transaction) throws CustomException{
        
        User user = userRepository.findUserById(transaction.getUserId());
        
        if(Helper.notNull(userRepository.findUserByEmail(user.getEmail()))){
            throw new CustomException("User already exists");
        }
        Wallet wallet = walletRepository.findWalletById(transaction.getUserId());

        if(transaction.getTransactionType() == TransactionType.income){
            wallet.setAmount(wallet.getAmount() + transaction.getAmount());
        }else{
            wallet.setAmount(wallet.getAmount() - transaction.getAmount());
        }

        return  walletRepository.save(wallet);
    }

    public Wallet getSingleWallet(String theId) throws DataNotExistException{
        Wallet walletSearch = walletRepository.findWalletById(theId);
        if(walletSearch == null){
            throw new DataNotExistException("Wallet is not present");
        }

        return walletSearch;
    }

}
