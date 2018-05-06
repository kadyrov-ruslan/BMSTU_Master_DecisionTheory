M = dlmread('hungarian.data.txt');
[rowsNum, colsNum]= size(M);
paramsCount = 14;
inputMatrix = zeros(rowsNum/10, paramsCount);

tempArr = zeros;
tempArrIdx = 1;
for j=1:rowsNum
    for i=1:colsNum
        tempArr(tempArrIdx) = M(j,i);
        tempArrIdx = tempArrIdx + 1;
    end
end

%Get only `paramsCount` important params
for j=1:(rowsNum/10)
    tempElements = tempArr((1 + (j - 1)*80):80*j);
    inputMatrix(j,1) = tempElements(1,3);
    inputMatrix(j,2) = tempElements(1,4);
    inputMatrix(j,3) = tempElements(1,9);
    inputMatrix(j,4) = tempElements(1,10);
    inputMatrix(j,5) = tempElements(1,12);
    inputMatrix(j,6) = tempElements(1,16);
    inputMatrix(j,7) = tempElements(1,19);
    inputMatrix(j,8) = tempElements(1,32);
    inputMatrix(j,9) = tempElements(1,38);
    inputMatrix(j,10) = tempElements(1,40);
    inputMatrix(j,11) = tempElements(1,41);
    inputMatrix(j,12) = tempElements(1,44);
    inputMatrix(j,13) = tempElements(1,51);
    inputMatrix(j,14) = tempElements(1,58);
end

Y3 = tsne(inputMatrix,'Algorithm','barneshut','NumPCAComponents',3,'NumDimensions',3);
[IDX,C] = kmeans(Y3,5);
figure
scatter3(Y3(:,1),Y3(:,2),Y3(:,3), 40,IDX, 'filled');