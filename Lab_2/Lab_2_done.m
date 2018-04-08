%Loading train and test datasets
load IrisTrainDataset.txt
TrainDataSet=IrisTrainDataset;
load IrisTestDataset.txt
TestDataSet=IrisTestDataset;

%Clustering train dataset
[IDX,C] = kmeans(TrainDataSet,3,'MaxIter',1000);
C = sortrows(C,4);

%Classyfing vectors from test dataset and register classyfing errors
[rowsNum, colsNum] = size(TestDataSet);
errorCount = 0;
errorVectors = zeros(rowsNum,colsNum);
for i = 1:rowsNum
    [n,d] = knnsearch(C,TestDataSet(i,:),'k',1);
    if(i <= rowsNum/3 && n~=1)
        errorCount = errorCount + 1;
        errorVectors(errorCount,:) = TestDataSet(i,:);
    elseif (i > rowsNum/3 && i <= rowsNum/3*2 && n~=2)
        errorCount = errorCount + 1;
        errorVectors(errorCount,:) = TestDataSet(i,:);
    elseif (i > rowsNum/3*2 && i <= rowsNum && n~=3)
        errorCount = errorCount + 1;
        errorVectors(errorCount,:) = TestDataSet(i,:);
    end
end
errorsPercantage = errorCount/rowsNum * 100;
fprintf('Errors: %2.4f percents \n',errorsPercantage);

%Creating 3d plots for train/test datasets and clusters' centroids
figure
h1 = scatter3(TrainDataSet(:,1),TrainDataSet(:,2),TrainDataSet(:,3), 40, IDX, 'filled');
hold on;
h2 = scatter3(TestDataSet(:,1),TestDataSet(:,2),TestDataSet(:,3), 40);
hold on;
h3 = scatter3(C(:,1),C(:,2),C(:,3), 55,'filled');
hold on;
legend([h1,h2, h3], 'Train dataset', 'Test dataset', 'Centroids');
xlabel SepalLength, ylabel SepalWidth, zlabel PetalLength